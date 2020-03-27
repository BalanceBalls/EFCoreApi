using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreApi.Controllers
{
	/// <summary>
	/// Демонстрационный контроллер.
	/// </summary>
	[Route("[controller]/[action]")]
	[ApiController]
	[AllowAnonymous]
	public class FooController : ControllerBase
	{
		/// <summary>
		/// Первый метод.
		/// </summary>
		/// <returns>Объект.</returns>
		[HttpGet]
		public async Task<IActionResult> Bar()
		{
			await AwaitActionAsync();
			return new JsonResult(new { foo="bar"});
		}
		
		/// <summary>
		/// Второй метод.
		/// </summary>
		/// <returns>Другой объект.</returns>
		[HttpGet]
		public async Task<IActionResult> Fizz()
		{ 
			await AwaitActionAsync();
			return new JsonResult(new { fizz = "buzz" });
		}

		/// <summary>
		/// Проверка работоспособности.
		/// </summary>
		/// <param name="payload">Нагрузочные данные.</param>
		/// <returns>Нагрузочные данные.</returns>
		[HttpPost]
		public async Task<IActionResult> Ping(string payload)
		{
			await AwaitActionAsync();
			return new JsonResult(new { payload });
		}

		/// <summary>
		/// Имитирует выполнение асинхронного запроса.
		/// </summary>
		private async Task AwaitActionAsync() => await Task.Delay(50);
	}
}