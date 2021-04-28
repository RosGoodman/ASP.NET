using System;

namespace MeticsManager.Models
{
    public class WeatherInitializer
    {
        /// <summary>Генератор нескольких показателей</summary>
        /// <param name="context"></param>
        public static void Seed(ValuesHolder context)
        {
            context.Weather.Add(new WeatherModel(55, Convert.ToDateTime("12-02-2055")));
            context.Weather.Add(new WeatherModel(49, Convert.ToDateTime("13-02-2055")));
            context.Weather.Add(new WeatherModel(43, Convert.ToDateTime("14-02-2055")));
            context.Weather.Add(new WeatherModel(40, Convert.ToDateTime("15-02-2055")));
            context.Weather.Add(new WeatherModel(35, Convert.ToDateTime("16-02-2055")));
        }
    }
}
