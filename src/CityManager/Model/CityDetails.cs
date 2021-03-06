using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CityManager.Model
{

    public class CityDetails : AdditionalCityDetails
    {
        /// <summary>
        /// City Name
        /// </summary>
        /// <value></value>
        [JsonPropertyName("name")]
        [Required(ErrorMessage = "City name is required")]
        [StringLength(200)]
        public string CityName { get; set; }

        /// <summary>
        /// City Sub Region Name
        /// </summary>
        /// <value></value>
        [JsonPropertyName("state")]
        [StringLength(200)]
        public string State { get; set; }

        /// <summary>
        /// Country Name
        /// </summary>
        /// <value></value>
        [JsonPropertyName("country")]
        [Required(ErrorMessage = "Country name is required")]
        [StringLength(200)]
        public string Country { get; set; }

    }

}