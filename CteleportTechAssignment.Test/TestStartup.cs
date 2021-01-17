using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlobalErrorHandling.Extensions;
using System.IO;
using CteleportTechAssignment.Persistence;
using CteleportTechAssignment.Cache;
using CteleportTechAssignment.Application;
using AutoMapper;

namespace CteleportTechAssignment.Test
{
	public class TestStartup
	{
		public TestStartup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			//My Custom services ..
			services.AddSingleton<IAPIService, APIMockService>();
			services.AddSingleton<ICacheService, CacheManager>();

			services.AddTransient<IAirportService, AirportService>();
			services.AddAutoMapper(typeof(MappingProfile));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILoggerFactory loggerFactory)
		{
			
			app.ConfigureExceptionHandler(loggerFactory.CreateLogger<Program>());

						

			var path = Directory.GetCurrentDirectory();
			loggerFactory.AddFile($"{path}\\Logs\\Log.txt");
		}
	}
}
