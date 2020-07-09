using CityManager.Controllers;
using CityManager.Service;
using Microsoft.Extensions.Logging;
using Moq;

namespace CityManager.Tests
{
    public class CityController_TestSetup
    {
        protected CityController _cityController;

        protected readonly Mock<ILogger<CityController>> _mockLogger;

        protected readonly Mock<ICityService> _mockService;

        public CityController_TestSetup()
        {
            _mockLogger = new Mock<ILogger<CityController>>();
            _mockService = new Mock<ICityService>();
        }
    }
}