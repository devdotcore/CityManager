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
    public class CountryService_GetCountry
    {
        private ICountryService _countryService;

        private readonly Mock<ILogger<CountryService>> _mockLogger = new Mock<ILogger<CountryService>>();

        private Mock<IRestApiClient<ICollection<CountryDetails>, CountryParams, string>> _mockClient = new Mock<IRestApiClient<ICollection<CountryDetails>, CountryParams, string>>();

        private readonly Mock<IOptions<AppSettings>> _mockOptions = new Mock<IOptions<AppSettings>>();


        public CountryService_GetCountry()
        {
            _mockOptions.Setup(o => o.Value).Returns(new AppSettings
            {
                CountriesApi = new CountriesApi
                {
                    Url = "http://test",
                    Service = "service",
                    Filters = "filters",
                    FullText = "fulltext"
                }
            });
        }

        [Fact]
        public async void Valid_CountryName_ReturnDetails()
        {
            //Given
            _mockClient.Setup(c => c.Get(It.IsAny<string>(), It.IsAny<CountryParams>())).ReturnsAsync(GetCountries());
            _countryService = new CountryService(_mockLogger.Object, _mockOptions.Object, _mockClient.Object);

            //When
            var response = await _countryService.GetCountryByNameAsync("Name");

            //Then
            Assert.Equal(GetCountries().FirstOrDefault().Alpha2Code, response.Alpha2Code);
            Assert.Equal(GetCountries().FirstOrDefault().Alpha3Code, response.Alpha3Code);
            Assert.Equal(GetCountries().FirstOrDefault().Name, response.Name);
            Assert.False(response.HasError);
        }

        [Fact]
        public async void Valid_CountryName_NotFound()
        {
            //Given
            _mockClient.Setup(c => c.Get(It.IsAny<string>(), It.IsAny<CountryParams>())).ReturnsAsync(GetCountries());
            _countryService = new CountryService(_mockLogger.Object, _mockOptions.Object, _mockClient.Object);

            //When
            var response = await _countryService.GetCountryByNameAsync("RandomCountry");

            //Then
            Assert.True(response.HasError);
            Assert.Equal(StatusCodes.NOT_FOUND, response.ServiceCode.Code);
        }

        private static List<CountryDetails> GetCountries()
        {
            return new List<CountryDetails> {
                new CountryDetails {
                    Alpha2Code = "2C",
                    Alpha3Code = "3C",
                    Currencies = new List<Currency> {
                        new Currency {
                            Code = "C"
                        }
                    },
                    Name = "Name"
                }
            };
        }
    }
}