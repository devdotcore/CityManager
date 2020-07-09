using System.Threading.Tasks;
using CityManager.Helper;
using CityManager.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Refit;

namespace CityManager.Service
{
    public class WeatherService : BaseService<WeatherService>, IWeatherService
    {
        /// <summary>
        /// Weather API Config
        /// </summary>
        private readonly WeatherApi _apiConfig;

        /// <summary>
        /// Rest client to call the the endpoint
        /// </summary>
        private readonly IRestApiClient<WeatherDetails, WeatherParams, string> _client;

        public WeatherService(ILogger<WeatherService> logger, IOptions<AppSettings> options, IRestApiClient<WeatherDetails, WeatherParams, string> client) : base(logger)
        {
            _apiConfig = options.Value.WeatherApi;
            _client = client;
        }

        public async Task<WeatherDetails> GetWeatherByCityNameAsync(string cityName)
        {
            try
            {
                _logger.LogDebug("Calling {methodName}, Getting weather for {cityName}", nameof(GetWeatherByCityNameAsync), cityName);

                WeatherParams weatherParams = new WeatherParams
                {
                    CityName = cityName,
                    ApiKey = _apiConfig.ApiKey,
                    Units = _apiConfig.Units
                };

                var response = await _client.Get($"{_apiConfig.Service}", weatherParams);

                if (response is null)
                {
                    _logger.LogError((int)StatusCodes.NOT_FOUND, "Api unable to find weather details for {cityName}.", cityName);
                    return GetServiceCode<WeatherDetails>(StatusCodes.NOT_FOUND);
                }

                return response;
            }
            catch (ValidationApiException validationApiException)
            {
                _logger.LogError(validationApiException, "HttpRequestException occurred while calling Weather api - {code} {Details}", (int)validationApiException.StatusCode, validationApiException.Message);
                return GetServiceCode<WeatherDetails>(StatusCodes.INVALID_REQUEST);
            }
            catch (ApiException exception)
            {
                _logger.LogError(exception, "Exception occurred while calling Weather api - {code} {Details}", (int)exception.StatusCode, exception.Message);
                return GetServiceCode<WeatherDetails>(StatusCodes.SYSTEM_ERROR);
            }
        }
    }
}