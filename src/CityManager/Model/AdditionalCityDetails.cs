using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CityManager.Model
{
    /// <summary>
    /// Additional city Properties that can be updated as well.
    /// </summary>
    public class AdditionalCityDetails : IValidatableObject
    {
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