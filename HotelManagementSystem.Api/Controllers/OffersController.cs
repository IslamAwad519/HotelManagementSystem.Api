using HotelManagementSystem.Api.DTOs.Offers;
using HotelManagementSystem.Api.Services.Offers;
using HotelManagementSystem.Api.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OffersController : ControllerBase
{
    private readonly IOfferService _facilityService;

    public OffersController(IOfferService facilityService)
    {
        _facilityService = facilityService;
    }


    [HttpGet]
    public async Task<ResultViewModel<IEnumerable<OfferDto>>> GetList()
    {
        var facilitiesDto = await _facilityService.GetAllFacilities();
        
        return new ResultViewModel<IEnumerable<OfferDto>>
        {
            IsSuccess = true,
            Data = facilitiesDto,
            Message = "request success"
        };
    }

    [HttpGet("{id}")]
    public async Task<ResultViewModel<OfferDto>> GetOffer([FromRoute] int id)
    {
        var facilityDto = await _facilityService.GetOffer(id);
        
        if (facilityDto is null)
        {
            return new ResultViewModel<OfferDto>
            {
                IsSuccess = false,
                Data = null,
                Message = "Bad request"
            };
        } 
        
        
        return new ResultViewModel<OfferDto>
        {
            IsSuccess = false,
            Data = facilityDto,
            Message = "Request success"
        };
    }
   
    [HttpPost]
    public async Task<ResultViewModel<OfferDto>> AddOffer([FromBody]CreateUpdateOfferDto createUpdateOfferDto)
    {
        var facilityDto = await _facilityService.AddOffer(createUpdateOfferDto);
        
        if (facilityDto is null)
        {
            return new ResultViewModel<OfferDto>
            {
                IsSuccess = false,
                Data = null,
                Message = "Bad request"
            };
        } 
        
        return new ResultViewModel<OfferDto>
        {
            IsSuccess = true,
            Data = facilityDto,
            Message = "Request success"
        };
    }
    
    [HttpPut("{id}")]
    public async Task<ResultViewModel<OfferDto>> UpdateOffer(int id, CreateUpdateOfferDto createUpdateOfferDto)
    {
        var facilityDto = await _facilityService.UpdateOffer(id, createUpdateOfferDto);
        
        if (facilityDto is null)
        {
            return new ResultViewModel<OfferDto>
            {
                IsSuccess = false,
                Data = null,
                Message = "Bad request"
            };
        }
        
        return new ResultViewModel<OfferDto>
        {
            IsSuccess = true,
            Data = facilityDto,
            Message = "Request success"
        };
    }

    [HttpDelete("{id}")]
    public async Task<ResultViewModel<OfferDto>> DeleteOffer(int id)
    {
        var result = await _facilityService.DeleteOffer(id);
        if (!result)
        {
            return new ResultViewModel<OfferDto>
            {
                IsSuccess = false,
                Message = "delete fail , object not found"
            };
        }
        return new ResultViewModel<OfferDto>
        {
            IsSuccess = true,
            Message = "delete success"
        };
    }

}
