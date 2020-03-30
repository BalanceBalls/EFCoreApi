using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EFCoreApi.Helpers
{
	public class ExceptionHandlerMiddleware
	{
		/// <summary>
		/// Следующий обработчик в цепочке.
		/// </summary>
		private readonly RequestDelegate _next;

		/// <summary>
		/// Логгер.
		/// </summary>
		private ILogger<ExceptionHandlerMiddleware> _logger;

		/// <summary>
		/// Конструктор.
		/// </summary>
		/// <param name="next">Следующий обработчик в цепочке.</param>
		/// <param name="logger">Логгер.</param>
		public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		/// <summary>
		/// Обрабатывает исключение.
		/// </summary>
		/// <param name="context">Контекст запроса.</param>
		/// <returns>Результат обработки.</returns>
		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next.Invoke(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		/// <summary>
		/// Обрабатывает исключение.
		/// </summary>
		/// <param name="context">Контекст исключения</param>
		/// <param name="exception">Исключение.</param>
		/// <returns>Результат обработки.</returns>
		private async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			_logger.LogError(exception, exception.StackTrace);
			var response = context.Response;
			var statusCode = (int)HttpStatusCode.BadRequest;
			response.ContentType = "application/json";
			response.StatusCode = statusCode;
			await response.WriteAsync(JsonConvert.SerializeObject(new { errorCode = -1, errorMessage = exception.Message}));
		}
	}
}