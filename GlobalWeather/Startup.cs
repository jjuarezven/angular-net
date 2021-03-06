﻿using GlobalWeather.services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Weather.Persistence;
using Weather.Persistence.Repositories;

namespace GlobalWeather
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
			CurrentDirectoryHelpers.SetCurrentDirectory();
			Log.Logger = new LoggerConfiguration()
			.ReadFrom.Configuration(Configuration)
			.CreateLogger();
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<DbContextSettings>(Configuration);
			//Inject logger
			services.AddSingleton(Log.Logger);
			services.InjectPersistence();
			services.InjectServices();
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "WeatherClient/dist";
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseSpaStaticFiles();
			app.UseHttpsRedirection();
			app.UseMvc();
			app.UseSpa(spa =>
			{
				spa.Options.SourcePath = "WeatherClient";
				if (env.IsDevelopment())
				{
					spa.UseAngularCliServer(npmScript: "start");
				}
			});
		}
	}
}
