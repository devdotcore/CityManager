using System;
using System.Collections.Generic;
using CityManager.Model;
using CityManager.Repository;

namespace CityManager.Tests
{
    public static class TestData
    {
        public static CityDetails GetCityDetails()
        {
            return new CityDetails
            {
                Country = "Test",
                EstimatedPopulation = 1010101010,
                CityName = "TestCity",
                State = "TestState",
                TouristRating = 5
            };
        }

        public static List<CountryDetails> GetCountries()
        {
            return new List<CountryDetails> {
                new CountryDetails {
                    Alpha2Code = "2C",
                    Alpha3Code = "3C",
                    Currencies = new List<Currency> {
                        new Currency {
                            Code = "C"
                        }
                    },
                    Name = "Name"
                }
            };
        }

        public static City GetCity()
        {
            return new City
            {
                CityId = 1,
                CityName = "Test",
                Country = new Country
                {
                    Name = "TestCountry",
                    Alpha2Code = "2C",
                    Alpha3Code = "3C",
                    CurrencyCode = "C"
                },
                EstimatedPopulation = 11111,
                State = "TestState",
                TouristRating = 1
            };
        }

        public static SearchResult GetSearchResult()
        {
            return new SearchResult
            {
                CityId = 1,
                CityName = "Test",
                CountryCode2Digit = "2C",
                CountryCode3Digit = "3C"
            };
        }

        public static WeatherDetails GetWeatherDetails()
        {
            return new WeatherDetails
            {
                Main = new Main
                {
                    Temp = 20.2m
                },
                Weather = new List<Weather>(){ new Weather {
                    Description = "Cloudy",
                    Main = "Cloudy"
                }
                }
            };
        }

    }
}