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
    public class CityService_DeleteCity : CityService_TestSetup
    {
        public CityService_DeleteCity()
        { }

        [Fact]
        public async void ValidDetails_ReturnSuccess()
        {
            //Given
            _mockRepository.Setup(c => c.Delete(It.IsAny<int>())).ReturnsAsync(TestData.GetCity());
           _cityService = new CityService(_mockCountryService.Object, _mockLogger.Object, _mockRepository.Object, _mockMapper.Object, _mockWeatherService.Object);
        
            //When
            var response = await _cityService.DeleteAsync(2);

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
            var response = await _cityService.DeleteAsync(2);

            //Then
            Assert.Equal(StatusCodes.NOT_FOUND, response.Code);
            Assert.Equal(StatusCodes.NOT_FOUND.GetDescription(), response.Message);          
        }

    }
}