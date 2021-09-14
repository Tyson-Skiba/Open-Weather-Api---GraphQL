using System;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WeatherApi
{
    public class OpenWeatherApiService: IOpenWeatherApiService
    {
        public HttpClient Client { get; }
   
        private readonly string _apiKey;

        public OpenWeatherApiService(HttpClient client, IConfiguration configuration)
        {
            _apiKey = configuration["ApiKey"];

            client.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/weather");

            Client = client;
        }

        private string ReadValue<T>(string enumValue) where T: System.Enum
        {
            var enumType = typeof(T);
            var memberInfos = enumType.GetMember(enumValue);
            var enumValueMemberInfo = memberInfos.FirstOrDefault(m => m.DeclaringType == enumType);
            var valueAttributes = enumValueMemberInfo.GetCustomAttributes(typeof(AmbientValueAttribute), false);
            return ((AmbientValueAttribute)valueAttributes[0]).Value as string;
        }

        private string GetOptions(Units? units = null, Language? language = null)
        {
            var options = string.Empty;

            if (units is { }) options += $"&units={ReadValue<Units>(units.ToString())}";
            if (language is { }) options += $"&units={ReadValue<Language>(language.ToString())}";

            return options;
        }

        public async Task<CurrentWeather> GetTodaysWeather(int cityId, Units? units = null, Language? language = null)
            => await Client.GetFromJsonAsync<CurrentWeather>($"?id={cityId}&appid={_apiKey}{GetOptions(units, language)}");
        

        public async Task<CurrentWeather> GetTodaysWeather(string cityName, Units? units = null, Language? language = null)
            => await Client.GetFromJsonAsync<CurrentWeather>($"?q={cityName}&appid={_apiKey}{GetOptions(units, language)}");
        
        public async Task<CurrentWeather> GetTodaysWeather(string cityName, string stateCode, Units? units = null, Language? language = null)
            => await Client.GetFromJsonAsync<CurrentWeather>($"?q={cityName},{stateCode}&appid={_apiKey}{GetOptions(units, language)}");
        
        public async Task<CurrentWeather> GetTodaysWeather(string cityName, string stateCode, string countryCode, Units? units = null, Language? language = null)
            => await Client.GetFromJsonAsync<CurrentWeather>($"?q={cityName},{stateCode},{countryCode}&appid={_apiKey}{GetOptions(units, language)}");

        public async Task<CurrentWeather> GetTodaysWeather(int lat, int lon, Units? units = null, Language? language = null)
            => await Client.GetFromJsonAsync<CurrentWeather>($"?lat={lat}&lon={lon}&appid={_apiKey}{GetOptions(units, language)}");
        
        public async Task<CurrentWeather> GetTodaysWeather(int zip, string countryCode, Units? units = null, Language? language = null)
            => await Client.GetFromJsonAsync<CurrentWeather>($"?zip={zip},{countryCode}&appid={_apiKey}{GetOptions(units, language)}");
    }
}
