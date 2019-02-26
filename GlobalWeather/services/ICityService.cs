using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Persistence.Models;

namespace GlobalWeather.services
{
	public interface ICityService
	{
		Task<City> GetLastAccessedCityAsync();
		Task UpdateLastAccessedCityAsync(City city);
	}
}
