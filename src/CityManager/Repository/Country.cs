using System.ComponentModel.DataAnnotations.Schema;
using CityManager.Model;

namespace CityManager.Repository
{
    public class Country
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CountryId { get; set; }

        public string Name { get; set; }

        public string Alpha2Code { get; set; }

        public string Alpha3Code { get; set; }

        public string CurrencyCode { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

    }
}