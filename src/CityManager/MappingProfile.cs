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
                    //Set currency code
                    dest.CurrencyCode = src?.Currencies?.FirstOrDefault()?.Code;
                });
        }
        
    }
}