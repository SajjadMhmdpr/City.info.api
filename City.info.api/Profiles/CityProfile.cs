using AutoMapper;
using City.info.api.Models;

namespace City.info.api.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile() {
            CreateMap<Entities.City, CityWithoutPointOfInterestDto>();
            CreateMap<Entities.City, CityDto>();
        }
    }
}
