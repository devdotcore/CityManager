using System.Collections.Generic;
using CityManager.Helper;
using CityManager.Model;
using CityManager.Service;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using System.Linq;

namespace CityManager.Tests
{
    public class WeatherService_GetWeather
    {
        private IWeatherService _weatherService;

        private readonly Mock<ILogger<WeatherService>> _mockLogger = new Mock<ILogger<WeatherService>>();

        private Mock<IRestApiClient<WeatherDetails, WeatherParams, string>> _mockClient = new Mock<IRestApiClient<WeatherDetails, WeatherParams, string>>();

        private readonly Mock<IOptions<AppSettings>> _mockOptions = new Mock<IOptions<AppSettings>>();


        public WeatherService_GetWeather()
        {
            _mockOptions.Setup(o => o.Value).Returns(new AppSettings
            {
                WeatherApi = new WeatherApi
                {
                    Url = "http://test",
                    Service = "service",
                    ApiKey = "APIKEY",
                    Units = "METRIC"
                }
            });
        }

        [Fact]
        public async void Valid_CityName_ReturnDetails()
        {
            //Given
            _mockClient.Setup(c => c.Get(It.IsAny<string>(), It.IsAny<WeatherParams>())).ReturnsAsync(TestData.GetWeatherDetails());
            _weatherService = new WeatherService(_mockLogger.Object, _mockOptions.Object, _mockClient.Object);

            //When
            var response = await _weatherService.GetWeatherByCityNameAsync("Name");

            //Then
            Assert.Equal(TestData.GetWeatherDetails().Main.Temp, response.Main.Temp);
        }

        [Fact]
        public async void Valid_CityName_NotFound()
        {
            //Given
            _weatherService = new WeatherService(_mockLogger.Object, _mockOptions.Object, _mockClient.Object);

            //When
            var response = await _weatherService.GetWeatherByCityNameAsync("Name");

            //Then
            Assert.Null(response);
        }
    }
}