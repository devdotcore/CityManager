using System.Threading.Tasks;
using CityManager.Model;

namespace CityManager.Service
{
    /// <summary>
    /// City Service will be dealing with all the CRUD operations related to an city
    /// </summary>
    public interface ICityService : IBaseService<CityService>
    {
        /// <summary>
        /// Add a city to local storage with the user provided details
        /// Validate an country - return error if invalid country
        /// </summary>
        /// <param name="cityDetails">City Details</param>
        /// <returns><see cref="ServiceCode"/></returns>
        Task<ServiceCode> AddAsync(CityDetails cityDetails);

        /// <summary>
        /// Update City Details based on city id
        /// </summary>
        /// <param name="id">Unique City Id</param>
        /// <param name="additionalCityDetails">Details to be updated</param>
        /// <returns><see cref="ServiceCode"/></returns>
        Task<ServiceCode> UpdateAsync(int id, AdditionalCityDetails additionalCityDetails);

    }
}