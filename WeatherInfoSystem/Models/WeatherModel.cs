using System.Collections.Generic;
using WeatherInfoSystem.ThirdParty;

namespace WeatherInfoSystem.Models
{
    public class WeatherModel
    {
        private double? _temperature;
        private double? _humidity;

        public WeatherModel(WeatherData data)
        {
            City = data.City;
            Country = data.Country;
            _temperature = (double) data.Temperature;
            _humidity = data.Humidity;
            Condition = data.Condition;
            Icons = data.Icons;
        }

        public string City { get; }

        public string Country { get; }

        public double? Temperature
        {
            get { return System.Math.Round((double) _temperature); }
        }

        public double? Humidity
        {
            get { return System.Math.Round((double) _humidity); }
        }

        public string Condition { get; }

        public IEnumerable<string> Icons { get; }
    }
}