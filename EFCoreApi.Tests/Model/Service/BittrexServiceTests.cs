using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreApi.Models.DataModel;
using EFCoreApi.Models.Exception;
using EFCoreApi.Models.Service;
using EFCoreApi.Models.ViewModel;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace EFCoreApi.Tests.Model.Service
{
	[Parallelizable]
	[TestFixture]
	public class BittrexServiceTests : BaseTest
	{
		private IBittrexService _bittrexService = default!;

		[SetUp]
		public new void SetUp()
		{
			_bittrexService = new BittrexService(TestApiDbContext);
		}

		[Test]
		public async Task GetPairsAsync_ReturnsExpected()
		{
			var expected = await FormDataAsync();
			var actual = await _bittrexService.GetPairsAsync();

			actual.Should().BeEquivalentTo(expected);
		}

		[Test]
		public async Task GetPairByIdAsync_CorrectId_ReturnsExpected()
		{
			var expected = (await FormDataAsync()).First();
			var actual = await _bittrexService.GetPairByIdAsync(expected.PairId);
			
			actual.Should().BeEquivalentTo(expected);
		}

		[Test]
		public async Task GetPairsByNameAsync_CorrectName_ReturnsExpected()
		{
			const string pairName = "BTC-USD";
			var expected = (await FormDataAsync()).Where(x => x.PairName == pairName);
			var actual = await _bittrexService.GetPairsByNameAsync(pairName);

			actual.Should().BeEquivalentTo(expected);
		}

		[Test]
		public async Task GetPairsByPeriodAsync_CorrectInterval_ReturnsExpected()
		{
			var expected = (await FormDataAsync()).First();
			var dateFrom = expected.Updated!.Value.AddDays(-1);
			var dateTo = expected.Updated!.Value.AddDays(1);

			var actual = await _bittrexService.GetPairsByPeriodAsync(dateFrom, dateTo);
			actual.Should().BeEquivalentTo(expected);
		}

		[Test]
		public async Task AddPairDataAsync_CorrectData_AddsEntry()
		{
			var testDate = new DateTime(2020, 9, 9);
			var expectedCurrencyData = new CreateOrUpdateCurrencyData
			{
				PairName = "XRP-USD",
				BuyPrice = 254.0m,
				SellPrice = 254.0m,
				LastTradePrice = 254.0m,
				HighPrice = 254.0m,
				LowPrice = 254.0m,
				Volume = 1234567,
				Updated = testDate
			};
			var actualBefore = await TestApiDbContext.Pairs.ToListAsync();
			var actualId = await _bittrexService.AddPairDataAsync(expectedCurrencyData);

			var actualAfter = await TestApiDbContext.Pairs.SingleAsync();

			actualBefore.Should().BeEmpty();
			actualAfter.PairId.Should().Be(actualId);
			actualAfter.Should().BeEquivalentTo(expectedCurrencyData);
		}

		[Test]
		public async Task UpdatedPairByIdAsync_CorrectId_UpdatesPair()
		{
			var testData = await FormDataAsync();
			
			var expectedCurrencyData = new CreateOrUpdateCurrencyData
			{
				PairName = "XRP-USD",
				BuyPrice = 258.0m,
				SellPrice = 259.0m,
				LastTradePrice = 260.0m,
				HighPrice = 261.0m,
				LowPrice = 262.0m,
				Volume = 1234568,
				Updated = DateTime.Now
			};

			var actualBefore = testData.First();
			actualBefore.Should().NotBeEquivalentTo(expectedCurrencyData);

			var id = actualBefore.PairId;

			await _bittrexService.UpdatedPairByIdAsync(id, expectedCurrencyData);

			var actualAfter = await TestApiDbContext.Pairs.SingleAsync(x => x.PairId == id);

			actualAfter.Should().BeEquivalentTo(expectedCurrencyData);
		}

		[Test]
		public void UpdatedPairByIdAsync_InCorrectId_ThrowsException()
		{
			Func<Task> act = async () => await _bittrexService.UpdatedPairByIdAsync(123, new CreateOrUpdateCurrencyData());

			act.Should().Throw<DataBaseException>();
		}

		[Test]
		public async Task DeletePairByIdAsync_DeletesItem()
		{
			var expected = await FormDataAsync();

			await _bittrexService.DeletePairByIdAsync(expected[0].PairId);

			var actual = await TestApiDbContext.Pairs.SingleAsync();
			
			actual.Should().BeEquivalentTo(expected[1]);
		}
		
		private async Task<List<PairData>> FormDataAsync()
		{
			var testDate1 = new DateTime(2020, 1, 1);
			var testDate2 = new DateTime(2020, 1, 5);
			
			_bittrexService = new BittrexService(TestApiDbContext);
			var testPairs = new List<PairData>
			{
				new PairData
				{
					PairName = "BTC-USD",
					BuyPrice = 254.0m,
					SellPrice = 254.0m,
					LastTradePrice = 254.0m,
					HighPrice = 254.0m,
					LowPrice = 254.0m,
					Volume = 1234567,
					Updated = testDate1
				},
				new PairData
				{
					PairName = "LTC-XRP",
					BuyPrice = 123.0m,
					SellPrice = 123.0m,
					LastTradePrice = 123.0m,
					HighPrice = 123.0m,
					LowPrice = 123.0m,
					Volume = 999999,
					Updated = testDate2
				}
			};

			await TestApiDbContext.Pairs.AddRangeAsync(testPairs);
			await TestApiDbContext.SaveChangesAsync();
			return testPairs;
		}

		private CurrencyAggregation GetBittrexData()
		{
			return new CurrencyAggregation
			{
				Pairs = new List<CurrencyData>
				{
					new CurrencyData
					{
						PairName = "ETH-USD",
						BuyPrice = 111.0m,
						SellPrice = 111.0m,
						LastTradePrice = 111.0m,
						HighPrice = 111.0m,
						LowPrice = 111.0m,
						Volume = 1234567,
						Updated = DateTime.Now
					}
				}
			};
		}
	}
}