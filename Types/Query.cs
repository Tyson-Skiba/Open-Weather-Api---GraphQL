using HotChocolate;
using System.Reflection;
using System.Threading.Tasks;

namespace WeatherApi
{
    public class Query
    {
        public string Version() => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public Task<CurrentWeather> GetTodaysWeatherById(
            [Service] IOpenWeatherApiService weatherApiService,
            int cityId,
            Units? units,
            Language? language
        ) => weatherApiService.GetTodaysWeather(cityId, units, language);
    }
}
