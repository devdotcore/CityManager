using System.Collections.Generic;
using System.Threading.Tasks;
using CityManager.Model;
using CityManager.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

        /// <summary>
        /// The purpose of this method is to add a new city to the DB
        /// - Validate request model based on -
        ///     - City name and Country name are mandatory
        ///     - Date should be less than current date
        ///     - Rating should be between range 1-5
        ///     - Country name should be valid - API will check against restcountries api to validate
        /// - Once the validation is successfull, city details along with additional country parameters will
        /// be stored locally for a faster retreval.
        /// </summary>
        /// <param name="cityDetails"><see cref="CityDetails"/></param>
        /// <returns><see cref="ServiceCode"/></returns>
        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(ServiceCode), (int)StatusCodes.SUCCESS)]
        [ProducesResponseType(typeof(ServiceCode), (int)StatusCodes.SYSTEM_ERROR)]
        [ProducesResponseType(typeof(ServiceCode), (int)StatusCodes.INVALID_REQUEST)]
        [ProducesResponseType(typeof(ServiceCode), (int)StatusCodes.NOT_FOUND)]
        [ProducesResponseType((int)StatusCodes.INVALID_REQUEST)]
        public async Task<ActionResult<ServiceCode>> PostAsync([FromBody] CityDetails cityDetails)
        {
            if (ModelState.IsValid)
            {
                ServiceCode response = await _cityService.AddAsync(cityDetails);
                return StatusCode((int)response.Code, response.Message);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [Route("update/{cityId}")]
        [ProducesResponseType(typeof(ServiceCode), (int)StatusCodes.SUCCESS)]
        [ProducesResponseType(typeof(ServiceCode), (int)StatusCodes.SYSTEM_ERROR)]
        [ProducesResponseType(typeof(ServiceCode), (int)StatusCodes.INVALID_REQUEST)]
        [ProducesResponseType(typeof(ServiceCode), (int)StatusCodes.NOT_FOUND)]
        [ProducesResponseType((int)StatusCodes.INVALID_REQUEST)]
        public async Task<ActionResult<ServiceCode>> PutAsync([FromRoute] int cityId, [FromBody] AdditionalCityDetails additionalCityDetails)
        {
            if (ModelState.IsValid)
            {
                ServiceCode response = await _cityService.UpdateAsync(cityId, additionalCityDetails);
                return StatusCode((int)response.Code, response.Message);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        [Route("delete/{cityId}")]
        [ProducesResponseType(typeof(ServiceCode), (int)StatusCodes.SUCCESS)]
        [ProducesResponseType(typeof(ServiceCode), (int)StatusCodes.SYSTEM_ERROR)]
        [ProducesResponseType(typeof(ServiceCode), (int)StatusCodes.INVALID_REQUEST)]
        [ProducesResponseType(typeof(ServiceCode), (int)StatusCodes.NOT_FOUND)]
        [ProducesResponseType((int)StatusCodes.INVALID_REQUEST)]
        public async Task<ActionResult<ServiceCode>> GetAsync([FromRoute] int cityId)
        {
            if (ModelState.IsValid)
            {
                ServiceCode response = await _cityService.DeleteAsync(cityId);
                return StatusCode((int)response.Code, response.Message);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        [Route("search/{cityName}")]
        [ProducesResponseType(typeof(ICollection<CityDetails>), (int)StatusCodes.SUCCESS)]
        [ProducesResponseType(typeof(ICollection<CityDetails>), (int)StatusCodes.SYSTEM_ERROR)]
        [ProducesResponseType(typeof(ICollection<CityDetails>), (int)StatusCodes.INVALID_REQUEST)]
        [ProducesResponseType(typeof(ICollection<CityDetails>), (int)StatusCodes.NOT_FOUND)]
        [ProducesResponseType((int)StatusCodes.INVALID_REQUEST)]
        public async Task<ActionResult<ICollection<CityDetails>>> SearchAsync([FromRoute]string cityName)
        {
            if (ModelState.IsValid || !string.IsNullOrEmpty(cityName))
            {
                var response = await _cityService.SearchAsync(cityName);
                return Ok(response);
            }
            else 
            {
                return BadRequest(ModelState);
            }
        }




    }
}