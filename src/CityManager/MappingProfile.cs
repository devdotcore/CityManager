using AutoMapper;
using CityManager.Model;
using CityManager.Repository;
using System.Linq;

namespace CityManager
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CityDetails, City>()
                .ForMember(dest => dest.Country, src => src.Ignore());
            
            CreateMap<CountryDetails, Country>()
                .AfterMap((src, dest) => {
                    //set currency code
                    dest.CurrencyCode = src?.Currencies?.FirstOrDefault()?.Code;
                });
            
            CreateMap<City, SearchResult>()
                .ForMember(dest =>  dest.WeatherDetails, src => src.Ignore())
                .ForMember(dest =>  dest.Country, src => src.Ignore())
                .AfterMap((src, dest) => {
                    
                    //set search result items
                    dest.CountryCode2Digit = src?.Country?.Alpha2Code;
                    dest.CountryCode3Digit = src?.Country?.Alpha3Code;
                    dest.CurrencyCode = src?.Country?.CurrencyCode;
                    dest.Country = src?.Country?.Name;

                });

        }
        
    }
}