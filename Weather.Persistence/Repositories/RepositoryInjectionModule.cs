﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Weather.Persistence.Repositories
{
	public static class RepositoryInjectionModule
	{
		/// <summary>
		///  Dependency inject DbContextFactory and CustomerRepository
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection InjectPersistence(this IServiceCollection services)
		{
			services.AddScoped<IDbContextFactory, DbContextFactory>();
			services.AddTransient<ICityRepository, CityRepository>();
			return services;
		}
	}
}
