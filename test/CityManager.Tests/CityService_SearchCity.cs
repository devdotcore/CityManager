using AutoMapper;
using CityManager.Helper;
using CityManager.Model;
using CityManager.Repository;
using CityManager.Service;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using System.Linq;
using System.Collections.Generic;

namespace CityManager.Tests
{
    public class CityService_SearchCity : CityService_TestSetup
    {
        public CityService_SearchCity()
        { }

        [Fact]
        public async void ValidDetails_ReturnSuccess()
        {
            //Given
            _mockRepository.Setup(c => c.GetAll()).ReturnsAsync(new List<City> { TestData.GetCity() });
            _mockMapper.Setup(m => m.Map<ICollection<SearchResult>>(It.IsAny<IEnumerable<City>>())).Returns(new List<SearchResult>() {TestData.GetSearchResult()});
   
           _cityService = new CityService(_mockCountryService.Object, _mockLogger.Object, _mockRepository.Object, _mockMapper.Object, _mockWeatherService.Object);
        
            //When
            var response = await _cityService.SearchAsync(TestData.GetCity().CityName);

            //Then
            Assert.Equal(TestData.GetSearchResult().CityId, response.FirstOrDefault().CityId);
            Assert.Equal(TestData.GetSearchResult().CountryCode2Digit, response.FirstOrDefault().CountryCode2Digit);   
            Assert.Equal(TestData.GetSearchResult().CountryCode3Digit, response.FirstOrDefault().CountryCode3Digit);      
        }

        [Fact]
        public async void ValidDetails_ReturnFailure()
        {
            //Given
            _mockMapper.Setup(m => m.Map<ICollection<SearchResult>>(It.IsAny<IEnumerable<City>>())).Returns(new List<SearchResult>() {TestData.GetSearchResult()});
   
           _cityService = new CityService(_mockCountryService.Object, _mockLogger.Object, _mockRepository.Object, _mockMapper.Object, _mockWeatherService.Object);
        
            //When
            var response = await _cityService.SearchAsync(TestData.GetCity().CityName);

            //Then
            Assert.Null(response);
          }

    }
}