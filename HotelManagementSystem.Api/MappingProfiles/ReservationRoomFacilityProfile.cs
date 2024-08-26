using AutoMapper;
using HotelManagementSystem.Api.DTOs.ReservationRoomFacilities;
using HotelManagementSystem.Api.Models;

namespace HotelManagementSystem.Api.MappingProfiles;

public class ReservationRoomFacilityProfile : Profile
{
    public ReservationRoomFacilityProfile()
    {
        CreateMap<ReservationRoomFacility, ReservationRoomFacilityDto>().ReverseMap();
    }
}
