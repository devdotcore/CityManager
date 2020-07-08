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
        /// <returns>Add City Response</returns>
        Task<AddCityResponse> AddAsync(CityDetails cityDetails);
    }
}