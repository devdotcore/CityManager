namespace CityManager
{
    public class AppSettings
    {
        /// <summary>
        /// Get configurations for countries api
        /// </summary>
        /// <value></value>
        public CountriesApi CountriesApi { get; set; }

        public WeatherApi WeatherApi { get; set; }
    }

    public class WeatherApi
    {
        /// <summary>
        /// Api Endpoint
        /// </summary>
        /// <value></value>
        public string Url { get; set; }

        /// <summary>
        /// Service Type
        /// </summary>
        /// <value></value>
        public string Service { get; set; }

        /// <summary>
        /// Api Key - unique to user
        /// </summary>
        /// <value></value>
        public string ApiKey { get; set; }

        /// <summary>
        /// Temperature Units
        /// </summary>
        /// <value></value>
        public string Units { get; set; }

    }

    public class CountriesApi
    {
        /// <summary>
        /// Api endpoint
        /// </summary>
        /// <value></value>
        public string Url { get; set; }

        /// <summary>
        /// Type of service used
        /// </summary>
        /// <value></value>
        public string Service { get; set; }

        /// <summary>
        /// Filters - fields required
        /// </summary>
        /// <value></value>
        public string Filters { get; set; }

        /// <summary>
        /// Type - Search by full text
        /// </summary>
        /// <value></value>
        public string FullText { get; set; }
    }
}