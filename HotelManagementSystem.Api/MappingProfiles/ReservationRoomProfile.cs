using AutoMapper;
using HotelManagementSystem.Api.DTOs.ReservationRooms;
using HotelManagementSystem.Api.Models;

namespace HotelManagementSystem.Api.MappingProfiles;

public class ReservationRoomProfile : Profile
{
    public ReservationRoomProfile()
    {
        CreateMap<ReservationRoom, ReservationRoomDto>().ReverseMap();
    }
}
