using GlobalWeather.services;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Threading.Tasks;
using Weather.Persistence.Models;

namespace GlobalWeather.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CitiesController : ControllerBase
	{
		private readonly ICityService _service;
		private readonly ILogger _logger;

		public CitiesController(ICityService service, ILogger logger)
		{
			_service = service;
			_logger = logger;
		}

		[HttpGet]
		public async Task<ActionResult<City>> Get()
		{
			var city = await _service.GetLastAccessedCityAsync();
			return city;
		}

		[HttpPost]
		public async Task Post([FromBody] City city)
		{
			await _service.UpdateLastAccessedCityAsync(city);
		}
	}
}