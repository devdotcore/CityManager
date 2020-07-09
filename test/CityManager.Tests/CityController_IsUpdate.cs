using CityManager.Controllers;
using CityManager.Helper;
using CityManager.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CityManager.Tests
{
    public class CityController_IsUpdate : CityController_TestSetup
    {
        [Fact]
        public async void Invalid_CityDetails_ReturnsBadRequest()
        {
            //Given
            var additionalDetails = new AdditionalCityDetails();
            _cityController = new CityController(_mockLogger.Object, _mockService.Object);
            _cityController.ModelState.AddModelError(nameof(additionalDetails.TouristRating), "Tourist Rating should be between 1-5");

            //When
            var response = await _cityController.PutAsync(1, new AdditionalCityDetails());
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
            _mockService.Setup(c => c.UpdateAsync(It.IsAny<int>(), It.IsAny<AdditionalCityDetails>())).ReturnsAsync(new ServiceCode((int)StatusCodes.SUCCESS));

            //When
            var response = await _cityController.PutAsync(1, new AdditionalCityDetails());
            var status = response.Result as ObjectResult;
           
            //Then
            Assert.Equal((int)StatusCodes.SUCCESS, status.StatusCode);
            Assert.Equal(StatusCodes.SUCCESS.GetDescription(), status.Value);
        }
    }
}