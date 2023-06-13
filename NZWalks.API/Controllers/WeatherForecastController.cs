using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherReportController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherReportController> _logger;

        public WeatherReportController(ILogger<WeatherReportController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherModel> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherModel
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}