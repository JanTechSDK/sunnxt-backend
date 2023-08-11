using AutoMapper;
using SunNxtBackend.Models;
using SunNxtBackend.ViewModels;

namespace SunNxtBackend
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            CreateMap<Country, CountryViewModel>().ReverseMap();
            CreateMap<State, StateViewModel>().ReverseMap();
            CreateMap<City, CityViewModel>().ReverseMap();
            CreateMap<AgeRange, AgeRangeViewModel>().ReverseMap();

            CreateMap<AppUser, AppUserViewModel>()
                   .ForMember(dest => dest.AgeRangeName, opt => opt.MapFrom(src => src.AgeRange.AgeRangeName))
                   .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.CountryName))
                   .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.State.StateName))
                   .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.CityName))
                   .ReverseMap();

            CreateMap<AppUser, AppUserRegisterViewModel>().ReverseMap();
            CreateMap<AppUser, AppUserLoginResponseViewModel>()
                   .ForMember(dest => dest.AgeRangeName, opt => opt.MapFrom(src => src.AgeRange.AgeRangeName))
                   .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.CountryName))
                   .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.State.StateName))
                   .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.CityName))
                   .ReverseMap();

        }

    }
}
