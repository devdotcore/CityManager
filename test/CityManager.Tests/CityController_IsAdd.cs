using System;
using CityManager.Controllers;
using CityManager.Model;
using CityManager.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using CityManager.Helper;

namespace CityManager.Tests
{
    public class CityController_IsAdd
    {
        private CityController _cityController;

        private readonly Mock<ILogger<CityController>> _mockLogger;

        private readonly Mock<ICityService> _mockService;

        public CityController_IsAdd()
        {
            _mockLogger = new Mock<ILogger<CityController>>();
            _mockService = new Mock<ICityService>();
        }

        [Fact]
        public async void Invalid_CityDetails_ReturnsBadRequest()
        {
            //Given
            CityDetails cityDetails = new CityDetails();
            _cityController = new CityController(_mockLogger.Object, _mockService.Object);
            _cityController.ModelState.AddModelError(nameof(cityDetails.Name), "City Name is Required");

            //When
            var response = await _cityController.PostAsync(cityDetails);
            var status = response.Result as ObjectResult;

            //Then
            Assert.Equal(StatusCodes.Status400BadRequest, status.StatusCode);
        }

        [Fact]
        public async void Valid_CityDetails_ReturnsSuccess()
        {
            //Given
            CityDetails cityDetails = GetCityDetails();
            _cityController = new CityController(_mockLogger.Object, _mockService.Object);
            _mockService.Setup(c => c.AddAsync(It.IsAny<CityDetails>())).ReturnsAsync(AddCityResponse.SUCCESS);

            //When
            var response = await _cityController.PostAsync(cityDetails);
            var status = response.Result as ObjectResult;
           
            //Then
            Assert.Equal(StatusCodes.Status201Created, status.StatusCode);
            Assert.Equal(AddCityResponse.SUCCESS.GetDescription(), status.Value);
        }

        [Fact]
        public async void Invalid_CountryName_ReturnsError()
        {
            //Given
            CityDetails cityDetails = GetCityDetails();
            _cityController = new CityController(_mockLogger.Object, _mockService.Object);
            _mockService.Setup(c => c.AddAsync(It.IsAny<CityDetails>())).ReturnsAsync(AddCityResponse.INVALID_COUNTRY);

            //When
            var response = await _cityController.PostAsync(cityDetails);
            var status = response.Result as ObjectResult;
           
            //Then
            Assert.Equal(StatusCodes.Status400BadRequest, status.StatusCode);
            Assert.Equal(AddCityResponse.INVALID_COUNTRY.GetDescription(), status.Value);
        }

        private static CityDetails GetCityDetails()
        {
            return new CityDetails
            {
                Country = "Test",
                DateEstablished = DateTime.UtcNow.Date.AddYears(-3),
                EstimatedPopulation = 1010101010,
                Name = "TestCity",
                State = "TestState",
                TouristRating = 5
            };
        }
    }
}