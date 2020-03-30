using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EFCoreApi.Models.DataModel;
using EFCoreApi.Models.ViewModel;

namespace EFCoreApi.Models.Service
{
	/// <summary>
	/// Интерфейс сервиса взаимодействия с Bittrex.
	/// </summary>
	public interface IBittrexService
	{
		/// <summary>
		/// Получает данные с Exmo API.
		/// </summary>
		/// <returns>Список данных о валютных парах на текущий момент.</returns>
		Task<List<CurrencyData>> GetDataAsync();

		/// <summary>
		/// Получает данные по валютной паре по идентификатору записи.
		/// </summary>
		/// <param name="id">Идентификатор записи.</param>
		/// <returns>Данные по валютной паре.</returns>
		Task<PairData> GetPairByIdAsync(long id);
		
		/// <summary>
		/// Получает данные по валютной паре.
		/// </summary>
		/// <param name="pairName">Название пары.</param>
		/// <returns>Список данных о валютной паре.</returns>
		Task<List<PairData>> GetPairsByNameAsync(string pairName);

		/// <summary>
		/// Получает данные о валютных парах за временной интервал.
		/// </summary>
		/// <param name="dateFrom">Начало интервала.</param>
		/// <param name="dateTo">Конец интервала.</param>
		/// <returns>Список данных о валютных парах за интервал.</returns>
		Task<List<PairData>> GetPairsByPeriodAsync(DateTime dateFrom, DateTime dateTo);

		/// <summary>
		/// Добавляет информацию о валютной паре.
		/// </summary>
		/// <param name="data">Информация о валютной паре.</param>
		/// <returns>Идентификатор созданной записи.</returns>
		Task<long> AddPairDataAsync(PairData data);

		/// <summary>
		/// Получает всю информацию о валютных парах из БД.
		/// </summary>
		/// <returns>Список данных о валютных парах.</returns>
		Task<List<PairData>> GetPairsAsync();

		/// <summary>
		/// Обновляет информацию о валютной паре.
		/// </summary>
		/// <param name="id">Идентификатор записи.</param>
		/// <param name="data">Информация о валютной паре.</param>
		/// <returns>Обновленная информация о валютной паре.</returns>
		Task<PairData> UpdatedPairByIdAsync(long id, UpdateCurrencyData data);

		/// <summary>
		/// Удаляет информацию о валютной паре.
		/// </summary>
		/// <param name="id">Идентификатор записи.</param>
		Task DeletePairByIdAsync(long id);
	}
}