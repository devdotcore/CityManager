using System.Collections.Generic;
using Refit;

namespace CityManager.Model
{
    /// <summary>
    /// Contract for RestCountries API
    /// </summary>
    public class CountryDetails : BaseModel
    {
        public CountryDetails() { }
        public CountryDetails(int code)
        {
            // Populate only when error
            if (code != (int)StatusCodes.SUCCESS)
            {
                this.HasError = true;
                this.ServiceCode = new ServiceCode
                {
                    Code = (StatusCodes)code
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
        public ICollection<Currency> Currencies { get; set; }

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

    }

    public class CountryParams
    {
        [AliasAs("fields")]
        public string Filter { get; set; }

        [AliasAs("fullText")]
        public string SearchByFullText { get; set; }
    }
}