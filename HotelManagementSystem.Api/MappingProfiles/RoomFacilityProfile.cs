using AutoMapper;
using HotelManagementSystem.Api.DTOs.RoomFacilities;
using HotelManagementSystem.Api.Models;

namespace HotelManagementSystem.Api.MappingProfiles;

public class RoomFacilityProfile :  Profile
{
    public RoomFacilityProfile()
    {
        CreateMap<RoomFacility, RoomFacilityDto>()
            .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.RoomId))
            .ForMember(dest => dest.FacilityId, opt => opt.MapFrom(src => src.FacilityId));
    }
}
