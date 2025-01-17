﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MS.DbContexts;
using MS.Entities;
using MS.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS.WebApi.Controllers
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
        private readonly IUnitOfWork<MSDbContext> unitOfWork;
        private readonly MSDbContext dbContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUnitOfWork<MSDbContext> unitOfWork,MSDbContext dbContext)
        {
            _logger = logger;
            this.unitOfWork = unitOfWork;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogTrace("WeatherForecast被调用");


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
