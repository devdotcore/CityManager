using System;
using CityManager.Controllers;
using CityManager.Model;
using CityManager.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using CityManager.Helper;

namespace CityManager.Tests
{
    public class CityController_IsAdd : CityController_TestSetup
    {
        [Fact]
        public async void Invalid_CityDetails_ReturnsBadRequest()
        {
            //Given
            CityDetails cityDetails = new CityDetails();
            _cityController = new CityController(_mockLogger.Object, _mockService.Object);
            _cityController.ModelState.AddModelError(nameof(cityDetails.CityName), "City Name is Required");

            //When
            var response = await _cityController.PostAsync(cityDetails);
            var status = response.Result as ObjectResult;

            //Then
            Assert.Equal((int)StatusCodes.INVALID_REQUEST, status.StatusCode);
        }

        [Fact]
        public async void Valid_CityDetails_ReturnsSuccess()
        {
            //Given
            CityDetails cityDetails = TestData.GetCityDetails();
            _cityController = new CityController(_mockLogger.Object, _mockService.Object);
            _mockService.Setup(c => c.AddAsync(It.IsAny<CityDetails>())).ReturnsAsync(new ServiceCode((int)StatusCodes.SUCCESS));

            //When
            var response = await _cityController.PostAsync(cityDetails);
            var status = response.Result as ObjectResult;
           
            //Then
            Assert.Equal((int)StatusCodes.SUCCESS, status.StatusCode);
            Assert.Equal(StatusCodes.SUCCESS.GetDescription(), status.Value);
        }

        [Fact]
        public async void Invalid_CountryName_ReturnsError()
        {
            //Given
            CityDetails cityDetails = TestData.GetCityDetails();
            _cityController = new CityController(_mockLogger.Object, _mockService.Object);
            _mockService.Setup(c => c.AddAsync(It.IsAny<CityDetails>())).ReturnsAsync(new ServiceCode((int)StatusCodes.NOT_FOUND));

            //When
            var response = await _cityController.PostAsync(cityDetails);
            var status = response.Result as ObjectResult;
           
            //Then
            Assert.Equal((int)StatusCodes.NOT_FOUND, status.StatusCode);
            Assert.Equal(StatusCodes.NOT_FOUND.GetDescription(), status.Value);
        }
    }
}