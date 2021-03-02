using Honeycomb.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HoneyBee.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHoneycombEventManager _eventManager;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHoneycombEventManager eventManager)
        {
            _logger = logger;
            _eventManager = eventManager;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            Thread.Sleep(10);

            stopWatch.Stop();
            _eventManager.AddData("api_response_ms", stopWatch.ElapsedMilliseconds);

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
