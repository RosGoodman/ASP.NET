using System.Collections.Generic;

namespace MeticsManager.Models
{
    public class ValuesHolder
    {
        public List<WeatherModel> Weather { get; set; }
        public ValuesHolder()
        {
            Weather = new List<WeatherModel>();
        }
    }
}
