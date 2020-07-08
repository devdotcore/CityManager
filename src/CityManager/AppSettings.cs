namespace CityManager
{
    public class AppSettings
    {   
        /// <summary>
        /// Get configurations for countries api
        /// </summary>
        /// <value></value>
        public CountriesApi CountriesApi { get; }
    }

    public class CountriesApi
    {
        /// <summary>
        /// Api endpoint
        /// </summary>
        /// <value></value>
        public string Url { get; }
        
        /// <summary>
        /// Type of service used
        /// </summary>
        /// <value></value>
        public string Service { get; }

        /// <summary>
        /// Filters - fields required
        /// </summary>
        /// <value></value>
        public string Filters { get; }

        /// <summary>
        /// Type - Search by full text
        /// </summary>
        /// <value></value>
        public bool FullText { get; }
    }
}