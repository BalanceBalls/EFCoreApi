using System;
using System.Threading.Tasks;
using EFCoreApi.Models.Service;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace EFCoreApi.Helpers
{
	/// <summary>
	/// Джоб для планировщика.
	/// </summary>
	public class UpdateDataJob : IUpdateDataJob
	{
		/// <summary>
		/// Логгер.
		/// </summary>
		private readonly ILogger<UpdateDataJob> _logger;

		/// <summary>
		/// Контекст подключения к EFCore БД.
		/// </summary>
		private readonly ApiDbContext _apiDbContext;

		/// <summary>
		/// Сервис взаимодействия с Bittrex.
		/// </summary>
		private readonly IBittrexService _bittrexService;

		/// <summary>
		/// Конструктор.
		/// </summary>
		/// <param name="logger">Логгер.</param>
		/// <param name="apiDbContext">Контекст подключения к EFCore БД.</param>
		/// <param name="bittrexService">Сервис взаимодействия с Bittrex.</param>
		public UpdateDataJob(ILogger<UpdateDataJob> logger, ApiDbContext apiDbContext, IBittrexService bittrexService)
		{
			_logger = logger;
			_apiDbContext = apiDbContext;
			_bittrexService = bittrexService;
		}

		/// <summary>
		/// Запустить задачу.
		/// </summary>
		/// <param name="token">Ключ отмены.</param>
		public async Task Run(IJobCancellationToken token)
		{
			token.ThrowIfCancellationRequested();
			await RunAtTimeOf(DateTime.Now);
		}

		/// <summary>
		/// Выполняет задачу.
		/// </summary>
		public async Task RunAtTimeOf(DateTime now)
		{
			_logger.LogInformation("Job started.");
			await _bittrexService.GetDataAsync();
			_logger.LogInformation("Job's done.");
		}
	}
}