using AutoMapper;
using HotelManagementSystem.Api.DTOs.Facilities;
using HotelManagementSystem.Api.Models;

namespace HotelManagementSystem.Api.MappingProfiles;

public class FacilityProfile : Profile
{
    public FacilityProfile()
    {
        CreateMap<CreateUpdateFacilityDto, Facility>();
        CreateMap<Facility, FacilityDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

        // Additional mappings
        CreateMap<RoomFacility, FacilityDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.FacilityId));
    }
}
