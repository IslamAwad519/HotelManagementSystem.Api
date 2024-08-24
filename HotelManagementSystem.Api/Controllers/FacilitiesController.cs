using HotelManagementSystem.Api.DTOs.Facilities;
using HotelManagementSystem.Api.Services.Facilities;
using HotelManagementSystem.Api.Services.Offers;
using HotelManagementSystem.Api.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FacilitiesController : ControllerBase
{
    private readonly IFacilityService _facilityService;

    public FacilitiesController(IFacilityService facilityService)
    {
        _facilityService = facilityService;
    }


    [HttpGet]
    public async Task<ResultViewModel<IEnumerable<FacilityDto>>> GetList()
    {
        var facilitiesDto = await _facilityService.GetAllFacilities();
        
        return new ResultViewModel<IEnumerable<FacilityDto>>
        {
            IsSuccess = true,
            Data = facilitiesDto,
            Message = "request success"
        };
    }

    [HttpGet("{id}")]
    public async Task<ResultViewModel<FacilityDto>> GetFacility([FromRoute] int id)
    {
        var facilityDto = await _facilityService.GetFacility(id);
        
        if (facilityDto is null)
        {
            return new ResultViewModel<FacilityDto>
            {
                IsSuccess = false,
                Data = null,
                Message = "Bad request"
            };
        } 
        
        
        return new ResultViewModel<FacilityDto>
        {
            IsSuccess = false,
            Data = facilityDto,
            Message = "Request success"
        };
    }
   
    [HttpPost]
    public async Task<ResultViewModel<FacilityDto>> AddFacility([FromBody]CreateUpdateFacilityDto createUpdateFacilityDto)
    {
        var facilityDto = await _facilityService.AddFacility(createUpdateFacilityDto);
        
        if (facilityDto is null)
        {
            return new ResultViewModel<FacilityDto>
            {
                IsSuccess = false,
                Data = null,
                Message = "Bad request"
            };
        } 
        
        return new ResultViewModel<FacilityDto>
        {
            IsSuccess = true,
            Data = facilityDto,
            Message = "Request success"
        };
    }
    
    [HttpPut("{id}")]
    public async Task<ResultViewModel<FacilityDto>> UpdateFacility(int id, CreateUpdateFacilityDto createUpdateFacilityDto)
    {
        var facilityDto = await _facilityService.UpdateFacility(id, createUpdateFacilityDto);
        
        if (facilityDto is null)
        {
            return new ResultViewModel<FacilityDto>
            {
                IsSuccess = false,
                Data = null,
                Message = "Bad request"
            };
        }
        
        return new ResultViewModel<FacilityDto>
        {
            IsSuccess = true,
            Data = facilityDto,
            Message = "Request success"
        };
    }

    [HttpDelete("{id}")]
    public async Task<ResultViewModel<FacilityDto>> DeleteFacility(int id)
    {
        var result = await _facilityService.DeleteFacility(id);
        if (!result)
        {
            return new ResultViewModel<FacilityDto>
            {
                IsSuccess = false,
                Message = "delete fail , object not found"
            };
        }
        return new ResultViewModel<FacilityDto>
        {
            IsSuccess = true,
            Message = "delete success"
        };
    }

}
