using System.Net;
using System.Threading.Tasks;
using CityManager.Model;
using CityManager.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CityManager.Helper;

namespace CityManager.Controllers
{
    [ApiController]
    [Produces(contentType: "application/json")]
    [Route("city/")]
    public class CityController : ControllerBase
    {
        /// <summary>
        /// The CityController logger
        /// </summary>
        private readonly ILogger<CityController> _logger;

        /// <summary>
        /// The City service
        /// </summary>
        private readonly ICityService _cityService;

        /// <summary>
        /// Initiates a new instance of <see cref="CityController" /> class.
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="cityService">City Service</param>
        public CityController(ILogger<CityController> logger, ICityService cityService)
        {
            _logger = logger;
            _cityService = cityService;
        }

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(AddCityResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AddCityResponse>> PostAsync([FromBody]CityDetails cityDetails)
        {
            if(ModelState.IsValid)
            {
                AddCityResponse response = await _cityService.AddAsync(cityDetails);
                return StatusCode((int)response, response.GetDescription());
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        
        
    }
}