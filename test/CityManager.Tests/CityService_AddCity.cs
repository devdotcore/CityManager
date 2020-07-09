using AutoMapper;
using CityManager.Helper;
using CityManager.Model;
using CityManager.Repository;
using CityManager.Service;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using System.Linq;

namespace CityManager.Tests
{
    public class CityService_AddCity : CityService_TestSetup
    {
        public CityService_AddCity()
        { }

        [Fact]
        public async void ValidDetails_ReturnSuccess()
        {
            //Given
            _mockCountryService.Setup(c => c.GetCountryByNameAsync(It.IsAny<string>())).ReturnsAsync(
                TestData.GetCountries().FirstOrDefault()
            );
            _mockMapper.Setup(x => x.Map<City>(It.IsAny<CityDetails>())).Returns(TestData.GetCity());
            _mockMapper.Setup(x => x.Map<Country>(It.IsAny<CountryDetails>())).Returns(TestData.GetCity().Country);
            _cityService = new CityService(_mockCountryService.Object, _mockLogger.Object, _mockRepository.Object, _mockMapper.Object, _mockWeatherService.Object);
        
            //When
            var response = await _cityService.AddAsync(TestData.GetCityDetails());

            //Then
            Assert.Equal(StatusCodes.SUCCESS, response.Code);
            Assert.Equal(StatusCodes.SUCCESS.GetDescription(), response.Message);          
        }

        [Fact]
        public async void ValidDetails_ReturnFailure()
        {
            //Given
            _mockCountryService.Setup(c => c.GetCountryByNameAsync(It.IsAny<string>())).ReturnsAsync(
                new Model.CountryDetails() {HasError = true, ServiceCode = new ServiceCode {
                    Code = StatusCodes.NOT_FOUND
                }}
            );
           _cityService = new CityService(_mockCountryService.Object, _mockLogger.Object, _mockRepository.Object, _mockMapper.Object, _mockWeatherService.Object);
        
            //When
            var response = await _cityService.AddAsync(TestData.GetCityDetails());

            //Then
            Assert.Equal(StatusCodes.NOT_FOUND, response.Code);
            Assert.Equal(StatusCodes.NOT_FOUND.GetDescription(), response.Message);          
        }

    }
}