using System.Collections.Generic;
using System.Threading.Tasks;
using CityManager.Model;

namespace CityManager.Service
{
    public interface ICountryService : IBaseService<CountryService>
    {
        /// <summary>
        /// Call the country endpoint defined in the appsetting for the api and get the list of counties by name.
        /// Using Refit to keep it real.
        /// Filters define in AppSetting and can be change with requirement - require contract change.
        /// </summary>
        /// <param name="countryName">Country Name</param>
        /// <returns>valid countries by name; if any</returns>
         Task<CountryDetails> GetCountryByNameAsync(string countryName);
    }
}