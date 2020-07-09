using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityManager.Repository
{
    public class City
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CityId { get; set; }

        public string CityName { get; set; }

        public string State { get; set; }

        public Country Country { get; set; }

        public short TouristRating { get; set; }

        public DateTime DateEstablished { get; set; }

        public long EstimatedPopulation { get; set; }
    }
}