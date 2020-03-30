using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreApi.Helpers;
using EFCoreApi.Models.DataModel;
using EFCoreApi.Models.Exception;
using EFCoreApi.Models.Repository;
using EFCoreApi.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using Refit;

namespace EFCoreApi.Models.Service
{
	/// <summary>
	/// Сервис взаимодействия с Bittrex.
	/// </summary>
	public class BittrexService : IBittrexService
	{
		/// <summary>
		/// Контекст подключения к БД.
		/// </summary>
		private readonly ApiDbContext _apiDbContext;
		
		/// <summary>
		/// Адрес API.
		/// </summary>
		private const string ApiUrl = "https://api.bittrex.com";

		/// <summary>
		/// Конструктор.
		/// </summary>
		/// <param name="apiDbContext">Контекст подключения к БД.</param>
		public BittrexService(ApiDbContext apiDbContext)
		{
			_apiDbContext = apiDbContext;
		}

		/// <summary>
		/// Получает данные с Exmo API.
		/// </summary>
		/// <returns>Список данных о валютных парах на текущий момент.</returns>
		public async Task<List<CurrencyData>> GetDataAsync()
		{
			var bittrexRestApi = RestService.For<IBittrexRestApi>(ApiUrl);
			try
			{
				var bittrexPairs = await bittrexRestApi.GetData();

				var pairsToAdd = bittrexPairs.Pairs.Select(x => new PairData
				{
					LowPrice = x.LowPrice,
					HighPrice = x.HighPrice,
					BuyPrice = x.BuyPrice,
					LastTradePrice = x.LastTradePrice,
					PairName = x.PairName,
					SellPrice = x.SellPrice,
					Updated = x.Updated,
					Volume = x.Volume
				});

				await _apiDbContext.Pairs.AddRangeAsync(pairsToAdd);
				await _apiDbContext.SaveChangesAsync();

				return bittrexPairs.Pairs!;
			}
			catch (ApiException e)
			{
				throw new BittrexException(e.Message);
			}
			catch (DbUpdateException e)
			{
				throw new DataBaseException(e.Message);
			}
		}

		/// <summary>
		/// Получает данные по валютной паре по идентификатору записи.
		/// </summary>
		/// <param name="id">Идентификатор записи.</param>
		/// <returns>Данные по валютной паре.</returns>
		public Task<PairData> GetPairByIdAsync(long id)
		{
			return _apiDbContext.Pairs.SingleOrDefaultAsync(x => x.PairId == id);
		}

		/// <summary>
		/// Получает данные по валютной паре.
		/// </summary>
		/// <param name="pairName">Название пары.</param>
		/// <returns>Список данных о валютной паре.</returns>
		public Task<List<PairData>> GetPairsByNameAsync(string pairName)
		{
			return _apiDbContext.Pairs.Where(x => x.PairName == pairName).ToListAsync();
		}

		/// <summary>
		/// Получает данные о валютных парах за временной интервал.
		/// </summary>
		/// <param name="dateFrom">Начало интервала.</param>
		/// <param name="dateTo">Конец интервала.</param>
		/// <returns>Список данных о валютных парах за интервал.</returns>
		public Task<List<PairData>> GetPairsByPeriodAsync(DateTime dateFrom, DateTime dateTo)
		{
			return _apiDbContext.Pairs.Where(x => x.Updated.HasValue && x.Updated.Value > dateFrom && x.Updated.Value < dateTo).ToListAsync();
		}

		/// <summary>
		/// Добавляет информацию о валютной паре.
		/// </summary>
		/// <param name="data">Информация о валютной паре.</param>
		/// <returns>Идентификатор созданной записи.</returns>
		public async Task<long> AddPairDataAsync(PairData data)
		{
			await _apiDbContext.Pairs.AddAsync(data);
			await _apiDbContext.SaveChangesAsync();

			return data.PairId;
		}

		/// <summary>
		/// Получает всю информацию о валютных парах из БД.
		/// </summary>
		/// <returns>Список данных о валютных парах.</returns>
		public Task<List<PairData>> GetPairsAsync()
		{
			return _apiDbContext.Pairs.ToListAsync();
		}

		/// <summary>
		/// Обновляет информацию о валютной паре.
		/// </summary>
		/// <param name="id">Идентификатор записи.</param>
		/// <param name="data">Информация о валютной паре.</param>
		/// <returns>Обновленная информация о валютной паре.</returns>
		public async Task<PairData> UpdatedPairByIdAsync(long id, UpdateCurrencyData data)
		{
			var pairToModify = await _apiDbContext.Pairs.SingleOrDefaultAsync(x => x.PairId == id);
			
			if (pairToModify == null) throw new DataBaseException("Could not update");

			_apiDbContext.Entry(pairToModify).CurrentValues.SetValues(data);  
			_apiDbContext.SaveChanges();
			return await _apiDbContext.Pairs.SingleAsync(x => x.PairId == id);
		}

		/// <summary>
		/// Удаляет информацию о валютной паре.
		/// </summary>
		/// <param name="id">Идентификатор записи.</param>
		public async Task DeletePairByIdAsync(long id)
		{
			var pairToRemove = await _apiDbContext.Pairs.SingleAsync(x => x.PairId == id);
			_apiDbContext.Pairs.Remove(pairToRemove);
			await _apiDbContext.SaveChangesAsync();
		}
	}
}