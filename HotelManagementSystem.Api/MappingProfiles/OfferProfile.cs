using AutoMapper;
using HotelManagementSystem.Api.DTOs.Offers;
using HotelManagementSystem.Api.Models;

namespace HotelManagementSystem.Api.MappingProfiles;

public class OfferProfile : Profile
{
    public OfferProfile()
    {
        CreateMap<CreateUpdateOfferDto, Offer>();
        CreateMap<Offer, OfferDto>();
    }
}
