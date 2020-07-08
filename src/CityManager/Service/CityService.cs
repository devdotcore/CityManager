using System.Threading.Tasks;
using CityManager.Model;
using Microsoft.Extensions.Logging;

namespace CityManager.Service
{
    /// <summary>
    /// City Service will be dealing with all the CRUD operations related to an city
    /// </summary>
    public class CityService : BaseService, ICityService
    {
        /// <summary>
        /// Service Logger
        /// </summary>
        private ILogger<CityService> _logger;

        /// <summary>
        /// The Country Service
        /// </summary>
        private ICountryService _countryService;

        public CityService(ILogger<CityService> logger, ICountryService countryService)
        {
            _logger = logger;
            _countryService = countryService;
        }

        /// <summary>
        /// Add a city to local storage with the user provided details
        /// Validate an country - return error if invalid country
        /// </summary>
        /// <param name="cityDetails">City Details</param>
        /// <returns>Add City Response</returns>
        public async Task<AddCityResponse> AddAsync(CityDetails cityDetails)
        {
            // Get related country details.
            var country = await _countryService.GetCountryByNameAsync(cityDetails.Country);

            // Country found when error is null
            if (country.Error is null)
            {
                return AddCityResponse.SUCCESS;
            }

            return AddCityResponse.INVALID_COUNTRY;
        }
    }
}