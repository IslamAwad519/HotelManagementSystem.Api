using HotelManagementSystem.Api.DTOs.RoomTypes;
using HotelManagementSystem.Api.Services.RoomTypes;
using HotelManagementSystem.Api.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RoomTypesController : ControllerBase
{
    private readonly IRoomTypeService _roomTypeService;

    public RoomTypesController(IRoomTypeService roomTypeService)
    {
        _roomTypeService = roomTypeService;
    }


    [HttpGet]
    public async Task<ResultViewModel<IEnumerable<RoomTypeDto>>> GetList()
    {
        var roomTypesDto = await _roomTypeService.GetAllRoomTypes();
        
        return new ResultViewModel<IEnumerable<RoomTypeDto>>
        {
            IsSuccess = true,
            Data = roomTypesDto,
            Message = "request success"
        };
    }

    [HttpGet("{id}")]
    public async Task<ResultViewModel<RoomTypeDto>> GetRoomType([FromRoute] int id)
    {
        var roomTypeDto = await _roomTypeService.GetRoomType(id);
        
        if (roomTypeDto is null)
        {
            return new ResultViewModel<RoomTypeDto>
            {
                IsSuccess = false,
                Data = null,
                Message = "Bad request"
            };
        } 
        
        
        return new ResultViewModel<RoomTypeDto>
        {
            IsSuccess = false,
            Data = roomTypeDto,
            Message = "Request success"
        };
    }
   
    [HttpPost]
    public async Task<ResultViewModel<RoomTypeDto>> AddRoomType([FromBody]CreateUpdateRoomTypeDto createUpdateRoomTypeDto)
    {
        var roomTypeDto = await _roomTypeService.AddRoomType(createUpdateRoomTypeDto);
        
        if (roomTypeDto is null)
        {
            return new ResultViewModel<RoomTypeDto>
            {
                IsSuccess = false,
                Data = null,
                Message = "Bad request"
            };
        } 
        
        return new ResultViewModel<RoomTypeDto>
        {
            IsSuccess = true,
            Data = roomTypeDto,
            Message = "Request success"
        };
    }
    
    [HttpPut("{id}")]
    public async Task<ResultViewModel<RoomTypeDto>> UpdateRoomType(int id, CreateUpdateRoomTypeDto createUpdateRoomTypeDto)
    {
        var roomTypeDto = await _roomTypeService.UpdateRoomType(id, createUpdateRoomTypeDto);
        
        if (roomTypeDto is null)
        {
            return new ResultViewModel<RoomTypeDto>
            {
                IsSuccess = false,
                Data = null,
                Message = "Bad request"
            };
        }
        
        return new ResultViewModel<RoomTypeDto>
        {
            IsSuccess = true,
            Data = roomTypeDto,
            Message = "Request success"
        };
    }

    [HttpDelete("{id}")]
    public async Task<ResultViewModel<RoomTypeDto>> DeleteRoomType(int id)
    {
        var result = await _roomTypeService.DeleteRoomType(id);
        if (!result)
        {
            return new ResultViewModel<RoomTypeDto>
            {
                IsSuccess = false,
                Message = "delete fail , object not found"
            };
        }
        return new ResultViewModel<RoomTypeDto>
        {
            IsSuccess = true,
            Message = "delete success"
        };
    }

}
