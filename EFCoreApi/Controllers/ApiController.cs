using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EFCoreApi.Helpers;
using EFCoreApi.Models.DataModel;
using EFCoreApi.Models.Service;
using EFCoreApi.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreApi.Controllers
{
	[Route("[controller]/[action]")]
	[AllowAnonymous]
	[ApiController]
	public class ApiController : Controller
	{
		/// <summary>
		/// Сервис взаимодействия с Bittrex.
		/// </summary>
		private IBittrexService _bittrexService;

		/// <summary>
		/// Конструктор.
		/// </summary>
		/// <param name="bittrexService">Сервис взаимодействия с Bittrex.</param>
		public ApiController(IBittrexService bittrexService)
		{
			_bittrexService = bittrexService;
		}

		/// <summary>
		/// Получает данные по валютной паре.
		/// </summary>
		/// <param name="pairName">Название пары.</param>
		/// <returns>Список данных о валютной паре.</returns>
		[HttpGet]
		public Task<List<PairData>> GetPairsByName(string pairName)
		{
			return _bittrexService.GetPairsByNameAsync(pairName);
		}

		/// <summary>
		/// Получает данные о валютных парах за временной интервал.
		/// </summary>
		/// <param name="dateFrom">Начало интервала.</param>
		/// <param name="dateTo">Конец интервала.</param>
		/// <returns>Список данных о валютных парах за интервал.</returns>
		[HttpGet]
		public Task<List<PairData>> GetPairsByPeriod(DateTime dateFrom, DateTime dateTo)
		{
			return _bittrexService.GetPairsByPeriodAsync(dateFrom, dateTo);
		}

		/// <summary>
		/// Добавляет информацию о валютной паре.
		/// </summary>
		/// <param name="data">Информация о валютной паре.</param>
		/// <returns>Идентификатор созданной записи.</returns>
		[HttpPost]
		public Task<long> AddPairData(PairData data)
		{
			return _bittrexService.AddPairDataAsync(data);
		}

		/// <summary>
		/// Получает всю информацию о валютных парах из БД.
		/// </summary>
		/// <returns>Список данных о валютных парах.</returns>
		[HttpGet]
		public Task<List<PairData>> GetPairs()
		{
			return _bittrexService.GetPairsAsync();
		}

		/// <summary>
		/// Обновляет информацию о валютной паре.
		/// </summary>
		/// <param name="id">Идентификатор записи.</param>
		/// <param name="data">Информация о валютной паре.</param>
		/// <returns>Обновленная информация о валютной паре.</returns>
		[HttpPost]
		public Task<PairData> UpdatedPairById(long id, UpdateCurrencyData data)
		{
			return _bittrexService.UpdatedPairByIdAsync(id, data);
		}

		/// <summary>
		/// Удаляет информацию о валютной паре.
		/// </summary>
		/// <param name="id">Идентификатор записи.</param>
		[HttpPost]
		public Task DeletePairById(long id)
		{
			return _bittrexService.DeletePairByIdAsync(id);
		}

		/// <summary>
		/// Получает данные по валютной паре по идентификатору записи.
		/// </summary>
		/// <param name="id">Идентификатор записи.</param>
		/// <returns>Данные по валютной паре.</returns>
		[HttpGet]
		public Task<PairData> GetPairByIdAsync(long id)
		{
			return _bittrexService.GetPairByIdAsync(id);
		}
	}
}