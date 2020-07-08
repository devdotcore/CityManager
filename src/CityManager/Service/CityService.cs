using System;
using System.Threading.Tasks;
using AutoMapper;
using CityManager.Model;
using CityManager.Repository;
using Microsoft.Extensions.Logging;

namespace CityManager.Service
{
    /// <summary>
    /// City Service will be dealing with all the CRUD operations related to an city
    /// </summary>
    public class CityService : BaseService<CityService>, ICityService
    {
        /// <summary>
        /// The Country Service
        /// </summary>
        private ICountryService _countryService;

        /// <summary>
        /// City CRUD operation repository
        /// </summary>
        private IRepository<City> _repository;

        public CityService(ICountryService countryService, ILogger<CityService> logger, IRepository<City> repository, IMapper mapper) : base(logger, mapper)
        {
            _countryService = countryService;
            _repository = repository;
        }

        /// <summary>
        /// Add a city to local storage with the user provided details
        /// Validate an country - return error if invalid country
        /// </summary>
        /// <param name="cityDetails">City Details</param>
        /// <returns>Add City Response</returns>
        public async Task<AddCityResponse> AddAsync(CityDetails cityDetails)
        {
            try
            {
                _logger.LogInformation("Validating county name..");
                // Get related country details.
                var country = await _countryService.GetCountryByNameAsync(cityDetails.Country);

                // Country found, when error is null
                if (country.Error is null)
                {
                    // Get the city details from the request object
                    var city = _mapper.Map<City>(cityDetails);

                    // Map the country details from country service API
                    city.Country = _mapper.Map<Country>(country);

                    _logger.LogInformation("Adding city details to the repository...");

                    await _repository.Add(city);

                    _logger.LogInformation("Successfully added city details to the repository");

                    return AddCityResponse.SUCCESS;
                }

                _logger.LogError("Invalid Country Name");

                return AddCityResponse.INVALID_COUNTRY;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong, check stack trace!!");
                return AddCityResponse.SYSTEM_ERROR;
            }
        }
    }
}