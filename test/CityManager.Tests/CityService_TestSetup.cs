using AutoMapper;
using CityManager.Helper;
using CityManager.Repository;
using CityManager.Service;
using Microsoft.Extensions.Logging;
using Moq;


namespace CityManager.Tests
{
    public class CityService_TestSetup
    {

        protected ICityService _cityService;

        protected readonly Mock<ILogger<CityService>> _mockLogger = new Mock<ILogger<CityService>>();

        protected Mock<ICountryService> _mockCountryService = new Mock<ICountryService>();

        protected Mock<IWeatherService> _mockWeatherService = new Mock<IWeatherService>();

        protected Mock<IRepository<City>> _mockRepository = new Mock<IRepository<City>>();
        
        protected Mock<IMapper> _mockMapper = new Mock<IMapper>();

        public CityService_TestSetup()
        {
            
        }
        
    }
}