using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CityManager.Model
{
    public enum AddCityResponse
    {
        [Description("City Details Successfully saved to local storage.")]
        SUCCESS = 201,

        [Description("Error while saving details - Invalid Country Name.")]
        INVALID_COUNTRY = 400,

        [Description("Error while saving details - System Exceptions, Check Logs.")]
        SYSTEM_ERROR = 500
    }

    public class CityDetails : IValidatableObject
    {
        /// <summary>
        /// City Name
        /// </summary>
        /// <value></value>
        [JsonPropertyName("name")]
        [Required(ErrorMessage = "City name is required")]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// City Sub Region Name
        /// </summary>
        /// <value></value>
        [JsonPropertyName("state")]
        public string State { get; set; }

        /// <summary>
        /// Country Name
        /// </summary>
        /// <value></value>
        [JsonPropertyName("country")]
        [Required(ErrorMessage = "Country name is required")]
        [StringLength(50)]
        public string Country { get; set; }

        /// <summary>
        /// Tourist Rating
        /// </summary>
        /// <value></value>
        [JsonPropertyName("touristRating")]
        [Range(1, 5, ErrorMessage = "Tourist Rating should be in the range 1-5")]
        public short TouristRating { get; set; }

        /// <summary>
        /// City establish date
        /// </summary>
        /// <value></value>
        [JsonPropertyName("dateEstablished")]
        public DateTime DateEstablished { get; set; }

        /// <summary>
        /// City Estimated Population
        /// </summary>
        /// <value></value>
        [JsonPropertyName("estimatedPopulation")]
        public long EstimatedPopulation { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateEstablished.Date >= DateTime.UtcNow.Date)
            {
                yield return new ValidationResult(
                    $"City established date cannot be more or equal to current date.",
                    new[] { nameof(DateEstablished) });
            }
        }
    }

}