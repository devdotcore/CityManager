using System.Collections.Generic;
using System.Threading.Tasks;
using CityManager.Helper;
using CityManager.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Refit;
using System.Linq;

namespace CityManager.Service
{
    /// <summary>
    /// Call the country endpoint defined in the appsetting for the api and get the list of counties by name.
    /// Using Refit to keep it real.
    /// Filters define in AppSetting and can be change with requirement - require contract change.
    /// </summary>
    public class CountryService : BaseService<CountryService>, ICountryService
    {
        /// <summary>
        /// Countries API Configurations
        /// </summary>
        private readonly CountriesApi _apiConfig;

        /// <summary>
        /// Rest client to call the the endpoint
        /// </summary>
        private readonly IRestApiClient<ICollection<CountryDetails>, CountryParams, string> _client;

        /// <summary>
        /// Initiates a new instance of <see cref="CountryService" /> class.
        /// </summary>
        /// <param name="logger">Service Logger</param>
        /// <param name="options">AppSetting </param>
        /// <param name="client">Rest Client</param>
        public CountryService(ILogger<CountryService> logger, IOptions<AppSettings> options, IRestApiClient<ICollection<CountryDetails>, CountryParams, string> client) : base(logger)
        {
            _apiConfig = options.Value.CountriesApi;
            _client = client;
        }

        /// <summary>
        /// Call the country endpoint defined in the appsetting for the api and get the list of counties by name.
        /// Using Refit to keep it real.
        /// Filters define in AppSetting and can be change with requirement - require contract change.
        /// </summary>
        /// <param name="countryName">Country Name</param>
        /// <returns> Valid country by name; if any; else error response</returns>
        public async Task<CountryDetails> GetCountryByNameAsync(string countryName)
        {
            try
            {
                _logger.LogDebug("Calling {methodName}, Getting countries for {name}", nameof(GetCountryByNameAsync), countryName);

                CountryParams countryParams = new CountryParams
                {
                    Filter = _apiConfig.Filters,
                    SearchByFullText = _apiConfig.FullText
                };

                var response = await _client.Get($"{_apiConfig.Service}/{countryName}", countryParams);

                _logger.LogInformation("{count} country(s) found. Getting Details - Filter Details {filter}", response.Count(), countryParams.Filter);

                //Safe check if the country name is in collection
                var country = response?.Where(x => x.Name.ToUpperInvariant() == countryName.ToUpperInvariant()).FirstOrDefault();
                if (country is null)
                {
                    _logger.LogError((int)StatusCodes.NOT_FOUND, "Country not found!");
                    return GetServiceCode<CountryDetails>(StatusCodes.NOT_FOUND);
                }

                return country;
            }
            catch (ValidationApiException validationApiException)
            {
                _logger.LogError(validationApiException, "HttpRequestException occurred while calling translation api - {code} {Details}", (int)validationApiException.StatusCode, validationApiException.Message);
                return GetServiceCode<CountryDetails>(StatusCodes.INVALID_REQUEST);
            }
            catch (ApiException exception)
            {
                _logger.LogError(exception, "Exception occurred while calling translation api - {code} {Details}", (int)exception.StatusCode, exception.Message);
                return GetServiceCode<CountryDetails>(StatusCodes.SYSTEM_ERROR);
            }

        }
    }
}