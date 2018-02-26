using System;
using System.Net;
using System.Web.Mvc;
using WeatherInfoSystem.Models;
using WeatherInfoSystem.ThirdParty;
using log4net;

namespace WeatherInfoSystem.Controllers
{
    public class HomeController : Controller
    {
        private IOpenWeatherManager weatherManager;

        private static ILog _logger = LogManager.GetLogger(typeof(HomeController));

        public HomeController(IOpenWeatherManager m)
        {
            weatherManager = m;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetWeatherData(string latitude, string longitude, int unit)
        {
            try
            {
                var weatherData = weatherManager.GetWeatherData(latitude, longitude, unit == 0 ? TemperatureUnit.Celcius : TemperatureUnit.Farenheit);

                return PartialView("_WeatherDataView", new WeatherModel(weatherData));
            }
            catch (Exception ex)
            {
                _logger.Error("Error to get current weather action", ex);
                throw new WebException("Failed to get current weather", ex);
            }
        }
    }
}