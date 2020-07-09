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
    public class CityService_UpdateCity : CityService_TestSetup
    {
        public CityService_UpdateCity()
        { }

        [Fact]
        public async void ValidDetails_ReturnSuccess()
        {
            //Given
            _mockRepository.Setup(c => c.Get(It.IsAny<int>())).ReturnsAsync(TestData.GetCity());
           _cityService = new CityService(_mockCountryService.Object, _mockLogger.Object, _mockRepository.Object, _mockMapper.Object, _mockWeatherService.Object);
        
            //When
            var response = await _cityService.UpdateAsync(2, new AdditionalCityDetails());

            //Then
            Assert.Equal(StatusCodes.SUCCESS, response.Code);
            Assert.Equal(StatusCodes.SUCCESS.GetDescription(), response.Message);          
        }

        [Fact]
        public async void ValidDetails_ReturnFailure()
        {
            //Given
            _cityService = new CityService(_mockCountryService.Object, _mockLogger.Object, _mockRepository.Object, _mockMapper.Object, _mockWeatherService.Object);
       
            //When
            var response = await _cityService.UpdateAsync(2, new AdditionalCityDetails());

            //Then
            Assert.Equal(StatusCodes.NOT_FOUND, response.Code);
            Assert.Equal(StatusCodes.NOT_FOUND.GetDescription(), response.Message);          
        }

    }
}