using CityManager.Controllers;
using CityManager.Helper;
using CityManager.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CityManager.Tests
{
    public class CityController_IsDelete : CityController_TestSetup
    {
        [Fact]
        public async void Invalid_CityId_ReturnsBadRequest()
        {
            //Given
            _cityController = new CityController(_mockLogger.Object, _mockService.Object);

            //When
            var response = await _cityController.DeleteAsync(-1);
            var status = response.Result as ObjectResult;

            //Then
            Assert.Equal((int)StatusCodes.INVALID_REQUEST, status.StatusCode);
        }

        [Fact]
        public async void Valid_CityId_ReturnsSuccess()
        {
            //Given
            CityDetails cityDetails = TestData.GetCityDetails();
            _cityController = new CityController(_mockLogger.Object, _mockService.Object);
            _mockService.Setup(c => c.DeleteAsync(It.IsAny<int>())).ReturnsAsync(new ServiceCode((int)StatusCodes.SUCCESS));

            //When
            var response = await _cityController.DeleteAsync(1);
            var status = response.Result as ObjectResult;

            //Then
            Assert.Equal((int)StatusCodes.SUCCESS, status.StatusCode);
            Assert.Equal(StatusCodes.SUCCESS.GetDescription(), status.Value);
        }
    }
}