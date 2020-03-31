using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.IO;
using System.Reflection;
using EFCoreApi.Helpers;
using EFCoreApi.Models.Repository;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Refit;

namespace EFCoreApi
{
	/// <summary>
	/// Класс запуска модуля.
	/// </summary>
	public class Startup
	{
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
			var connectionString = _configuration.GetSection("ConnectionStrings")["DefaultConnection"];
			services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddDbContext<ApiDbContext>(options => options.UseSqlServer(connectionString));

			services.AddSingleton<IMemoryCache, MemoryCache>();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo  { Title = "EFCoreApi", Version = "v1" });
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
			});

			services.AddHangfire(conf =>
			{
				var options = new SqlServerStorageOptions
				{
					PrepareSchemaIfNecessary = false,
					QueuePollInterval = TimeSpan.FromMinutes(5)
				};
				conf.UseSqlServerStorage(connectionString, options);
			});
			services.AddScoped<IUpdateDataJob, UpdateDataJob>();

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

			app.UseHangfireServer(new BackgroundJobServerOptions
			{
				WorkerCount = 1
			});
			
			app.UseRouting();
			app.UseHealthChecks("/health");
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseMiddleware<ExceptionHandlerMiddleware>();
			
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
			
			GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute{ Attempts = 0 });
			JobScheduler.ScheduleRecurringJobs();
			
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
