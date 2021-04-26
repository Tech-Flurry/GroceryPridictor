using GroceryPridictor.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryPridictor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : CustomControllerBase
    {
        private static readonly string[] _summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        public ILogger<WeatherForecastController> Logger => _logger;

        [HttpGet]
        public IActionResult Get()
        {
            var rng = new Random();
            return Ok(() =>
            {
                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = _summaries[rng.Next(_summaries.Length)]
                })
                .ToArray();
            });
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var rng = new Random();
            return await Ok(async () =>
            {
                return await Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = _summaries[rng.Next(_summaries.Length)]
                })
                .ToArray());
            });
        }
        [HttpGet]
        public IActionResult GetWithNoData()
        {
            return NoDataFound();
        }
        [HttpPost]
        public IActionResult Save(WeatherForecast model)
        {
            return DataUpdated();
        }
    }
}
