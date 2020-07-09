namespace CityManager.Model
{
    public class SearchResult : CityDetails
    {
        /// <summary>
        /// City Id
        /// </summary>
        /// <value></value>
        public int CityId { get; set; }

        /// <summary>
        /// 2 Digit Country Code
        /// </summary>
        /// <value></value>
        public string CountryCode2Digit { get; set; }

        /// <summary>
        /// 3 Digit Country Code
        /// </summary>
        /// <value></value>
        public string CountryCode3Digit { get; set; }

        /// <summary>
        /// Currency Code
        /// </summary>
        /// <value></value>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Weather Details
        /// </summary>
        /// <value></value>
        public WeatherDetails WeatherDetails { get; set; }
    }
}