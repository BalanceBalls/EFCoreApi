using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace EFCoreApi
{
	/// <summary>
	/// Класс запуска модуля.
	/// </summary>
	public class Startup
	{
		#region Constants

		/// <summary>
		/// Название переменной окружения хранящей логин для подключения к БД.
		/// </summary>
		private const string DB_LOGIN = "DB_LOGIN";

		/// <summary>
		/// Название переменной окружения хранящей пароль для подключения к БД.
		/// </summary>
		private const string DB_PASSWORD = "DB_PASSWORD";

		#endregion

		/// <summary>
		/// Интерфейс для работы с конфигурацией.
		/// </summary>
		private readonly IConfiguration _configuration;

		public Startup(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		/// <summary>
		/// This method gets called by the runtime. Use this method to add services to the container.
		/// </summary>
		public void ConfigureServices(IServiceCollection services)
		{
			services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			
			services.AddSingleton<IMemoryCache, MemoryCache>();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo  { Title = "EFCoreApi", Version = "v1" });
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
			});

			services.AddAuthorization();
			services.AddHealthChecks();
			services.AddControllers();
		}

		/// <summary>
		/// Конфигурируем DI.
		/// </summary>
		/// <param name="builder"></param>
		public void ConfigureContainer(ContainerBuilder builder)
		{
			builder.RegisterModule(new EFCoreApiAutofacModule());
		}

		/// <summary>
		/// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		/// </summary>
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseForwardedHeaders();
			app.UsePathBase(_configuration.GetSection("BaseUrl").Value);

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/error");
			}
			
			SetConnectionString();
			
			app.UseRouting();
			app.UseHealthChecks("/health");
			app.UseAuthentication();
			app.UseAuthorization();

			if (env.IsDevelopment())
			{
				// Enable middleware to serve generated Swagger as a JSON endpoint.
				app.UseSwagger();
				
				// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
				// specifying the Swagger JSON endpoint.
				app.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint("/swagger/v1/swagger.json", "EFCoreApi V1");
				});
			}

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		/// <summary>
		/// Устанавливает строку соединения в БД.
		/// </summary>
		private void SetConnectionString()
		{
			var login = Environment.GetEnvironmentVariable(DB_LOGIN);
			var password = Environment.GetEnvironmentVariable(DB_PASSWORD);

			//HttpContextSqlConnectionService.ConnectionString = $"{_configuration.GetConnectionString("Default")} User Id={login};Password={password};";
		}
	}
}
