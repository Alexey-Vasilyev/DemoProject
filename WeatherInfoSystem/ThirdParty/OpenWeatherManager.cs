using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Configuration;
using Newtonsoft.Json;
using log4net;

namespace WeatherInfoSystem.ThirdParty
{
    public class OpenWeatherManager: IOpenWeatherManager
    {
        private const string CHANGEFROM = "amp;";
        private const string CHANGETO = "&";

        private static ILog _logger = LogManager.GetLogger(typeof(OpenWeatherManager));

        private string WeatherRequest;
        private string WeatherIcon;
        private string AppId;

        public OpenWeatherManager()
        {
            AppId = ConfigurationManager.AppSettings["OpenWeatherId"];
            WeatherRequest = ConfigurationManager.AppSettings["OpenWeatherWebRequest"];
            WeatherIcon = ConfigurationManager.AppSettings["OpenWeatherIcon"];
        }

        public WeatherData GetWeatherData(string latitude, string longitude, TemperatureUnit unit)
        {
            _logger.Info($"Location coordinates: ({latitude}, {longitude})");

            var weatherData = new WeatherData
            {
                Unit = unit
            };

            WebResponse response = GetResponse(latitude, longitude, weatherData);
            string responseData = string.Empty;

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                responseData = sr.ReadToEnd();
                _logger.Debug($"The response from the weather server: `{responseData}`");
            }

            ParseResponse(weatherData, responseData);

            return weatherData;
        }

        private WebResponse GetResponse(string latitude, string longitude, WeatherData weatherData)
        {
            var webRequest = string.Format(WeatherRequest.Replace(CHANGEFROM, CHANGETO), latitude, longitude, weatherData.Unit == TemperatureUnit.Celcius ? "metric" : "imperial", AppId);
            return WebRequest.Create(webRequest).GetResponse();
        }

        private void ParseResponse(WeatherData weatherData, string responseData)
        {
            if (!string.IsNullOrEmpty(responseData))
            {
                try
                {
                    dynamic json = JsonConvert.DeserializeObject<dynamic>(responseData);

                    var weather = json.weather as System.Collections.Generic.IEnumerable<dynamic> ?? new dynamic[] { };

                    weatherData.City = (string)json.name;
                    weatherData.Country = (string)json.sys.country;
                    weatherData.Temperature = (double)json?.main?.temp;
                    weatherData.Humidity = (double)json?.main?.humidity;
                    weatherData.Condition = string.Join(", ", weather.Select(w => (string)w.main));
                    weatherData.Icons = weather.Select(w => string.Format(WeatherIcon, (string)w.icon));
                }
                catch (Exception ex)
                {
                    string icouldnot = "Unable to get weather data from the response";
                    _logger.Error(icouldnot, ex);
                    throw new Exception(icouldnot, ex);
                }
            }

            if (string.IsNullOrEmpty(weatherData.Condition))
            {
                weatherData.Condition = "Undefined";
                _logger.Warn($"No weather data are available");
            }
        }
    }
}