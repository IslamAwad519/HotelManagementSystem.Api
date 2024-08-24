using HotelManagementSystem.Api.DTOs.Offers;

namespace HotelManagementSystem.Api.Services.Offers;

public interface IOfferService
{
    Task<IEnumerable<OfferDto>> GetAllFacilities();
    Task<OfferDto?> GetOffer(int offerId);
    Task<OfferDto> AddOffer(CreateUpdateOfferDto offerDto);
    Task<OfferDto> UpdateOffer(int offerId, CreateUpdateOfferDto createUpdateOfferDto);
    Task<bool> DeleteOffer(int offerId);
}
