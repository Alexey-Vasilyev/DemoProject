namespace WeatherInfoSystem.ThirdParty
{
    public interface IOpenWeatherManager
    {
        WeatherData GetWeatherData(string latitude, string longitude, TemperatureUnit unit);
    }
}
