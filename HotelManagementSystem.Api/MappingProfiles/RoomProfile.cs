using AutoMapper;
using HotelManagementSystem.Api.DTOs;
using HotelManagementSystem.Api.DTOs.Facilities;
using HotelManagementSystem.Api.DTOs.Images;
using HotelManagementSystem.Api.DTOs.Rooms;
using HotelManagementSystem.Api.Models;

namespace HotelManagementSystem.Api.MappingProfiles;

public class RoomProfile :Profile
{
    public RoomProfile()
    {
        CreateMap<CreateUpdateRoomDto, Room>()
            .ForMember(e => e.Facilities, opt => opt.Ignore());

        CreateMap<Room, RoomDto>();



        ////.ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images)
        //.ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(f => new ImageDto()
        //{
        //    Id = f.Id,
        //    FilePath = f.FilePath,
        //    RoomId = f.RoomId
        //})));


        //CreateMap<Room, RoomDto>()
        //    .ForMember(dest => dest.Facilities, opt => opt.MapFrom(src => src.Facilities.Select(f => new RoomFacilityDto()
        //    {
        //        FacilityId = f.FacilityId,
        //        RoomId = f.RoomId
        //    })))

    }
}

