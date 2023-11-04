using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System;

namespace bdd_obligatorio_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private IConfiguration _config;
        private MySqlConnection _conn;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IConfiguration config, ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _config = config;
            _conn = new MySqlConnection(config.GetConnectionString("Default"));
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            await _conn.OpenAsync();
            // Retrieve all rows
            using var command = new MySqlCommand("SELECT name FROM Weather", _conn);
            using var reader = await command.ExecuteReaderAsync();

            List<WeatherForecast> weathers = new List<WeatherForecast> { };
            while (await reader.ReadAsync())
                {
                weathers.Add(new WeatherForecast(reader.GetString(0)));
                }

            return weathers;
        }
    }
}