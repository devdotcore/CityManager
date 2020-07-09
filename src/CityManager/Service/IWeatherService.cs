using System.Threading.Tasks;
using CityManager.Model;

namespace CityManager.Service
{
    public interface IWeatherService : IBaseService<WeatherService>
    {
        /// <summary>
        /// Call openweathermap to get the current weather details by city name
        /// </summary>
        /// <param name="cityName">city name</param>
        /// <returns></returns>
         Task<WeatherDetails> GetWeatherByCityNameAsync(string cityName);
    }
}