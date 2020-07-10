using System.Collections.Generic;
using System.Threading.Tasks;
using CityManager.Model;
using CityManager.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CityManager.Controllers
{
    /// <summary>
    /// City - Basic CRUD operations
    ///  - Add          
    ///    Validate request model based on -
    ///     - City name and Country name are mandatory
    ///     - Date should be less than current date
    ///     - Rating should be between range 1-5
    ///     - Country name should be valid - API will check against restcountries api to validate
    ///  - Update
    ///    Additional Details to be updated based on city id
    ///  - Delete
    ///    Delete an city by city id
    ///  - Search
    ///    Search a city by name - return all matching details + current weather details
    /// </summary>
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
        /// Add a city to the database. Once the validation is successfull. city details along with additional country parameters will
        /// be stored locally for a faster retreval.
        /// </summary>
        /// <param name="cityDetails"><see cref="CityDetails"/></param>
        /// <returns><see cref="ServiceCode"/></returns>
        [HttpPost]
        [Route("")]
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

        /// <summary>
        /// Update Additional city details by city id
        /// </summary>
        /// <param name="cityId">city id</param>
        /// <param name="additionalCityDetails"><see cref="AdditionalCityDetails"/></param>
        /// <returns><see cref="ServiceCode"/></returns>
        [HttpPut]
        [Route("{cityId}")]
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

        /// <summary>
        /// Delete a city details by id
        /// </summary>
        /// <param name="cityId">city id</param>
        /// <returns><see cref="ServiceCode"/></returns>
        [HttpDelete]
        [Route("{cityId}")]
        [ProducesResponseType(typeof(ServiceCode), (int)StatusCodes.SUCCESS)]
        [ProducesResponseType(typeof(ServiceCode), (int)StatusCodes.SYSTEM_ERROR)]
        [ProducesResponseType(typeof(ServiceCode), (int)StatusCodes.INVALID_REQUEST)]
        [ProducesResponseType(typeof(ServiceCode), (int)StatusCodes.NOT_FOUND)]
        [ProducesResponseType((int)StatusCodes.INVALID_REQUEST)]
        public async Task<ActionResult<ServiceCode>> DeleteAsync([FromRoute] int cityId)
        {
            if (ModelState.IsValid && cityId > 0)
            {
                ServiceCode response = await _cityService.DeleteAsync(cityId);
                return StatusCode((int)response.Code, response.Message);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Search a city by city name, return all details
        /// call weather api for getting the current weather
        /// </summary>
        /// <param name="cityName">city name</param>
        /// <returns>Collection of <see cref="SearchResult"/></returns>
        [HttpGet]
        [Route("{cityName}")]
        [ProducesResponseType(typeof(ICollection<SearchResult>), (int)StatusCodes.SUCCESS)]
        [ProducesResponseType(typeof(ICollection<SearchResult>), (int)StatusCodes.SYSTEM_ERROR)]
        [ProducesResponseType(typeof(ICollection<SearchResult>), (int)StatusCodes.INVALID_REQUEST)]
        [ProducesResponseType(typeof(ICollection<SearchResult>), (int)StatusCodes.NOT_FOUND)]
        [ProducesResponseType((int)StatusCodes.INVALID_REQUEST)]
        public async Task<ActionResult<ICollection<SearchResult>>> GetAsync([FromRoute] string cityName)
        {
            if (ModelState.IsValid && !string.IsNullOrEmpty(cityName))
            {
                var response = await _cityService.SearchAsync(cityName.Trim());
                return Ok(response);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}