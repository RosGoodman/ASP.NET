
using System;

namespace MeticsManager.Models
{
    public class WeatherModel
    {
        /// <summary>Дата измерения температуры.</summary>
        public DateTime Date { get; set; }
        /// <summary>Температура С.</summary>
        public int TemperatureC { get; set; }
        /// <summary>Температура F.</summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public WeatherModel(int temperatureC, DateTime date)
        {
            TemperatureC = temperatureC;
            Date = date;
        }
    }
}
