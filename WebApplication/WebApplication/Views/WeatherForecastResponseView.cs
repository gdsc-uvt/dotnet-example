﻿using System;

namespace WebApplication.Views
{
    public class WeatherForecastResponseView
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }
        
        public string Test { get; set; }
    }
}