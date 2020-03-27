using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;

namespace EFCoreApi
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
			try
			{
				logger.Debug("Старт модуля.");
				CreateHostBuilder(args).Build().Run();
			}
			catch (Exception exception)
			{
				logger.Error(exception, "Произошло исключение. Работа модуля остановлена.");
				throw;
			}
			finally
			{
				NLog.LogManager.Shutdown();
			}
		}

		/// <summary>
		/// Создает хост модуля.
		/// </summary>
		/// <param name="args">Аргументы запуска.</param>
		/// <returns>Хост модуля <see cref="IWebHost" />.</returns>
		private static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureAppConfiguration((hostingContext, config) =>
				{
					config.AddEnvironmentVariables();
				})
				.UseServiceProviderFactory(new AutofacServiceProviderFactory())
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.ConfigureLogging(logging => { logging.ClearProviders(); })
					.UseNLog()
					.ConfigureServices(services => services.AddAutofac())
					.UseStartup<Startup>();
				});
	}
}
