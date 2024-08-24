using AutoMapper;
using HotelManagementSystem.Api.DTOs.RoomTypes;
using HotelManagementSystem.Api.Models;

namespace HotelManagementSystem.Api.MappingProfiles;

public class RoomTypeProfile : Profile
{
    public RoomTypeProfile()
    {
        CreateMap<CreateUpdateRoomTypeDto, RoomType>();
        CreateMap<RoomType, RoomTypeDto>();
    }
}
