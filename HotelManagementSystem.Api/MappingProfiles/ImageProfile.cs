using AutoMapper;
using HotelManagementSystem.Api.DTOs.Images;
using HotelManagementSystem.Api.Models;

namespace HotelManagementSystem.Api.MappingProfiles;

public class ImageProfile : Profile
{
    public ImageProfile()
    {
        CreateMap<Image, ImageDto>()
            .ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => src.FilePath));
    }
}
