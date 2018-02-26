using System.Collections.Generic;

namespace WeatherInfoSystem.ThirdParty
{
    public enum TemperatureUnit { Celcius, Farenheit }

    public class WeatherData
    {
        public WeatherData()
        {
            Icons = new string[] { };
        }

        public string City { set; get; }

        public string Country { set; get; }

        public double? Temperature { set;  get; }

        public TemperatureUnit Unit { get; set; }

        public double? Humidity { set;  get; }

        public string Condition { get; set; }

        public IEnumerable<string> Icons { get; set; }
    }
}