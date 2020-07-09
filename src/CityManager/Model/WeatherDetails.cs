using System.Collections.Generic;
using Refit;

namespace CityManager.Model
{   
    /// <summary>
    /// Contract for openweathermap
    /// </summary>
    public class WeatherDetails
    {
        /// <summary>
        /// Weather from openweathermap
        /// </summary>
        /// <value></value>
        public ICollection<Weather> Weather { get; set; }
        
        /// <summary>
        /// Temperature
        /// </summary>
        /// <value></value>
        public Main Main { get; set; }
        
    }

    public class Main
    {
        /// <summary>
        /// Current Temperature
        /// </summary>
        /// <value></value>
        public decimal Temp { get; set; }
    }

    public class Weather
    {
        /// <summary>
        /// Weather Type
        /// </summary>
        /// <value></value>
        public string Main { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        /// <value></value>
        public string Description { get; set; }
    }

    public class WeatherParams
    {
        [AliasAs("q")]
        public string CityName { get; set; }

        [AliasAs("units")]
        public string Units { get; set; }

        [AliasAs("appid")]
        public string ApiKey { get; set; }
    }
}