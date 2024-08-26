using AutoMapper;
using HotelManagementSystem.Api.DTOs.Reservations;
using HotelManagementSystem.Api.Models;

namespace HotelManagementSystem.Api.MappingProfiles;

public class ReservationProfile : Profile
{
    public ReservationProfile()
    {
        CreateMap<CreateReservationDto, Reservation>()
            .ForMember(e => e.RoomFacilities, opt => opt.Ignore())
            .ForMember(e => e.Rooms, opt => opt.Ignore())
            .ForMember(e => e.TotalAmount, opt => opt.Ignore());


        CreateMap<Reservation, ReservationDto>()
            .ForMember(dest=>dest.PaymentIntentId,opt=>opt.MapFrom(src=>src.PaymentIntentId));
    }
}

