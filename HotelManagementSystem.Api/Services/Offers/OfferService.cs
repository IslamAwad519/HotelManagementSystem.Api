using AutoMapper;
using HotelManagementSystem.Api.DTOs.Offers;
using HotelManagementSystem.Api.Models;
using HotelManagementSystem.Api.Repository;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Api.Services.Offers;

public class OfferService : IOfferService
{
    private readonly IRepository<Offer> _offerRepository;
    private readonly IMapper _mapper;

    public OfferService(
        IMapper mapper, 
        IRepository<Offer> offerRepository)
    {
        _mapper = mapper;
        _offerRepository = offerRepository;
    }

    public async Task<IEnumerable<OfferDto>> GetAllFacilities()
    {
        var offers = await  _offerRepository.GetAll().ToListAsync();
        return  _mapper.Map<IEnumerable<OfferDto>>(offers);
    }

    public async Task<OfferDto?> GetOffer(int offerId)
    {
        var offer = await _offerRepository.GetById(offerId);
        return _mapper.Map<OfferDto>(offer);
    }

    public async Task<OfferDto> AddOffer(CreateUpdateOfferDto createOfferDto)
    {
        var offer = _mapper.Map<Offer>(createOfferDto);
       await _offerRepository.AddAsync(offer);

       return _mapper.Map<OfferDto>(offer);
    }

    public async Task<OfferDto> UpdateOffer(int offerId, CreateUpdateOfferDto createUpdateOfferDto)
    {
        var offer = await _offerRepository.GetById(offerId);
        if (offer is null)
        {
            return null;
        }

        _mapper.Map(createUpdateOfferDto, offer);
       await _offerRepository.Update(offer);
       return _mapper.Map<OfferDto>(offer);
    }

    public async Task<bool> DeleteOffer(int offerId)
    {
        var offer = await _offerRepository.GetById(offerId);
        if (offer is null)
        {
            return false;
        }
        await _offerRepository.Delete(offer);
        return true;
    }
}
