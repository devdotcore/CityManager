using System.Collections.Generic;
using CityManager.Controllers;
using CityManager.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CityManager.Tests
{
    public class CityController_IsSearch : CityController_TestSetup
    {
        [Fact]
        public async void Invalid_CityName_ReturnsBadRequest()
        {
            //Given
            _cityController = new CityController(_mockLogger.Object, _mockService.Object);

            //When
            var response = await _cityController.GetAsync(null);
            var status = response.Result as ObjectResult;

            //Then
            Assert.Equal((int)StatusCodes.INVALID_REQUEST, status.StatusCode);
        }

        [Fact]
        public async void Valid_CityName_ReturnsSuccess()
        {
            //Given
            CityDetails cityDetails = TestData.GetCityDetails();
            _cityController = new CityController(_mockLogger.Object, _mockService.Object);
            _mockService.Setup(c => c.SearchAsync(It.IsAny<string>())).ReturnsAsync(new List<SearchResult>());

            //When
            var response = await _cityController.GetAsync("city");
            var status = response.Result as ObjectResult;

            //Then
            Assert.Equal((int)StatusCodes.SUCCESS, status.StatusCode);
            Assert.IsType<List<SearchResult>>(status.Value);
        }
    }
}