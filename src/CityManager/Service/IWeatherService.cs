using System.Threading.Tasks;
using CityManager.Model;

namespace CityManager.Service
{
    public interface IWeatherService : IBaseService<WeatherService>
    {
         Task<WeatherDetails> GetWeatherByCityNameAsync(string cityName);
    }
}