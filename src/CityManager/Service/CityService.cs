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
        public async Task<ServiceCode> AddAsync(CityDetails cityDetails)
        {
            try
            {
                _logger.LogInformation("Validating county name..");
                // Get related country details.
                var country = await _countryService.GetCountryByNameAsync(cityDetails.Country);

                // check if response has any errors
                if (!country.HasError)
                {
                    // Get the city details from the request object
                    var city = _mapper.Map<City>(cityDetails);

                    // Map the country details from country service API
                    city.Country = _mapper.Map<Country>(country);

                    _logger.LogInformation("Adding city details to the repository...");

                    await _repository.Add(city);

                    _logger.LogInformation("Successfully added city details to the repository");

                    return GetServiceCode<ServiceCode>(StatusCodes.SUCCESS);
                }

                _logger.LogError("Invalid Country Name");

                return country.ServiceCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong, check stack trace!!");
                return GetServiceCode<ServiceCode>(StatusCodes.SYSTEM_ERROR); ;
            }
        }

        public async Task<ServiceCode> DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation("Looking for city by id - {id}", id);
                City city = await _repository.Get(id);

                if (!(city is null))
                {
                    _logger.LogInformation("City found, deleting now...");

                    await _repository.Delete(id);

                    _logger.LogInformation("City deleted successfully");

                    return GetServiceCode<ServiceCode>(StatusCodes.SUCCESS);
                }

                _logger.LogError("Unable to find city with the id - {id}", id);

                return GetServiceCode<ServiceCode>(StatusCodes.NOT_FOUND);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong, check stack trace!!");
                return GetServiceCode<ServiceCode>(StatusCodes.SYSTEM_ERROR); ;
            }
        }

        public async Task<ServiceCode> UpdateAsync(int id, AdditionalCityDetails additionalCityDetails)
        {
            try
            {
                _logger.LogInformation("Looking for city by id - {id}", id);
                City city = await _repository.Get(id);

                if (!(city is null))
                {
                    _logger.LogInformation("City found, updating now...");

                    city.DateEstablished = additionalCityDetails.DateEstablished;
                    city.EstimatedPopulation = additionalCityDetails.EstimatedPopulation;
                    city.TouristRating = additionalCityDetails.TouristRating;

                    await _repository.Update(city);

                    _logger.LogInformation("City details updated successfully");

                    return GetServiceCode<ServiceCode>(StatusCodes.SUCCESS);
                }

                _logger.LogError("Unable to find city with the id - {id}", id);

                return GetServiceCode<ServiceCode>(StatusCodes.NOT_FOUND);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong, check stack trace!!");
                return GetServiceCode<ServiceCode>(StatusCodes.SYSTEM_ERROR); ;
            }
        }
    }
}