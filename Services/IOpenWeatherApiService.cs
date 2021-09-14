using System.Threading.Tasks;

namespace WeatherApi
{
    public interface IOpenWeatherApiService
    {
        Task<CurrentWeather> GetTodaysWeather(int cityId, Units? units = null, Language? language = null);
        Task<CurrentWeather> GetTodaysWeather(string cityName, Units? units = null, Language? language = null);
        Task<CurrentWeather> GetTodaysWeather(string cityName, string stateCode, Units? units = null, Language? language = null);
        Task<CurrentWeather> GetTodaysWeather(string cityName, string stateCode, string countryCode, Units? units = null, Language? language = null);
        Task<CurrentWeather> GetTodaysWeather(int lat, int lon, Units? units = null, Language? language = null);
        Task<CurrentWeather> GetTodaysWeather(int zip, string countryCode, Units? units = null, Language? language = null);
    }
}
