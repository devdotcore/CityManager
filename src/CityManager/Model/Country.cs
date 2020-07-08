using System.Collections.Generic;
using Refit;

namespace CityManager.Model
{
    /// <summary>
    /// Contract for RestCountries API
    /// </summary>
    public class Country : BaseModel
    {
        public Country(int code, string message)
        {
            if(code > 0)
            {
                this.Error = new Error {
                    Code = code,
                    Message = message
                };
            }
        }

        /// <summary>
        /// Country Name
        /// </summary>
        /// <value></value>
        public string Name { get; set; }

        /// <summary>
        /// Currencies for the country
        /// </summary>
        /// <value></value>
        public List<Currency> Currencies { get; set; }

        /// <summary>
        /// Country Code 2 Digit
        /// </summary>
        /// <value></value>
        public string Alpha2Code { get; set; }

        /// <summary>
        /// Country Code 3 Digit
        /// </summary>
        /// <value></value>
        public string Alpha3Code { get; set; }
    }

    public class Currency
    {
        /// <summary>
        /// Currency Code
        /// </summary>
        /// <value></value>
        public string Code { get; set; }

        /// <summary>
        /// Currency Name
        /// </summary>
        /// <value></value>
        public string Name { get; set; }

        /// <summary>
        /// Currency Symbol
        /// </summary>
        /// <value></value>
        public string Symbol { get; set; }

    }

    public class CountryFieldsFilter
    {
        [AliasAs("fields")]
        public string Filter { get; set; }
    }
}