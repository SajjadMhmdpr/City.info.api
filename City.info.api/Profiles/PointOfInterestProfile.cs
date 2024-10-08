using AutoMapper;
using City.info.api.Entities;
using City.info.api.Models;

namespace City.info.api.Profiles
{
    public class PointOfInterestProfile : Profile
    {
        public PointOfInterestProfile() {

            CreateMap<PointOfInterest,PointOfInterestDto>();
            CreateMap<PointOfInterest,PointOfInterestForUpdateDto>();
            CreateMap<PointOfInterestForCreateDto,PointOfInterest>();
            CreateMap<PointOfInterestForUpdateDto,PointOfInterest>();
        
        } 
    }
}
