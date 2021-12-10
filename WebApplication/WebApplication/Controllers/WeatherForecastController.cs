using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private static readonly List<WeatherForecastModel> Database = GetModels();

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecastResponseView> Get()
        {
            return Database.Select(weatherForecast => new WeatherForecastResponseView
            {
                Id = weatherForecast.Id,
                Date = weatherForecast.Date,
                TemperatureC = weatherForecast.TemperatureC,
                Summary = weatherForecast.Summary,
                Test = weatherForecast.Test
            });
        }

        [HttpGet("{id}")]
        public WeatherForecastResponseView Get(string id)
        {
            var model = Database.Find(item => item.Id == id);
            if (model is null)
            {
                throw new ArgumentException("No model found!");
            }

            return new WeatherForecastResponseView
            {
                Id = model.Id,
                Date = model.Date,
                TemperatureC = model.TemperatureC,
                Summary = model.Summary
            };
        }

        [HttpDelete]
        public WeatherForecastResponseView Delete(string id)
        {
            var model = Database.Find(item => item.Id == id);
            if (model is null)
            {
                throw new ArgumentException("No model found!");
            }

            Database.Remove(model);

            return new WeatherForecastResponseView
            {
                Id = model.Id,
                Date = model.Date,
                TemperatureC = model.TemperatureC,
                Summary = model.Summary
            };
        }

        [HttpPost]
        public WeatherForecastResponseView Post(WeatherForecastRequestView entity)
        {
            var model = new WeatherForecastModel
            {
                Id = Guid.NewGuid().ToString(),
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Date = entity.Date,
                TemperatureC = entity.TemperatureC,
                Summary = entity.Summary
            };

            Database.Add(model);
            var existing = Database.Find(item => item.Id == model.Id);
            return new WeatherForecastResponseView
            {
                Id = existing.Id,
                Date = existing.Date,
                TemperatureC = existing.TemperatureC,
                Summary = existing.Summary
            };
        }

        private static List<WeatherForecastModel> GetModels()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecastModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)],
                    Test = "test"
                })
               .ToList();
        }
    }
}