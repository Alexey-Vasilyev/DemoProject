using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherInfoSystem.ThirdParty;

namespace Test.WeatherInfoSystem
{
    [TestClass]
    public class OpenWeatherManagerTests
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void OpenWeatherManagerWrongLocationTest()
        {
            var openWeatherManager = new OpenWeatherManager();

            // intentionally giving a downright wrong location
            openWeatherManager.GetWeatherData("845", "579", TemperatureUnit.Celcius);
        }

        [TestMethod]
        public void OpenWeatherManagerCheckNYTest()
        {
            var openWeatherManager = new OpenWeatherManager();
            var weatherData = openWeatherManager.GetWeatherData("40.7", "-74", TemperatureUnit.Celcius);

            // assuming hat if we get a wrong location our weather data will likely be wrong as well
            Assert.IsTrue(weatherData.City.Equals("New York"), "Failed to get data for New York");
        }
    }
}
