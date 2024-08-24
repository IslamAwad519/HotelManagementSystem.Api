using HotelManagementSystem.Api.DTOs.Facilities;
using HotelManagementSystem.Api.DTOs.Rooms;
using HotelManagementSystem.Api.Services.Rooms;
using HotelManagementSystem.Api.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet]
    public async Task<ResultViewModel<IEnumerable<RoomDto>>> GetList()
    {
        var roomsDto = await _roomService.GetAllRooms();
        
        return new ResultViewModel<IEnumerable<RoomDto>>
        {
            IsSuccess = true,
            Data = roomsDto,
            Message = "request success"
        };
    }

    [HttpGet("{id}")]
    public async Task<ResultViewModel<RoomDto>> GetRoom(int id)
    {
        var roomDto = await _roomService.GetRoom(id);
        
        if (roomDto is null)
        {
            return new ResultViewModel<RoomDto>
            {
                IsSuccess = false,
                Data = null,
                Message = "Bad request"
            };
        } 
        
        
        return new ResultViewModel<RoomDto>
        {
            IsSuccess = false,
            Data = roomDto,
            Message = "Request success"
        };
    }
   
    [HttpPost]
    public async Task<ResultViewModel<RoomDto>> AddRoom([FromForm]CreateUpdateRoomDto createRoomDto, List<IFormFile> images)
    {
        var roomDto = await _roomService.AddRoom(createRoomDto, images);
        
        if (roomDto is null)
        {
            return new ResultViewModel<RoomDto>
            {
                IsSuccess = false,
                Data = null,
                Message = "Bad request"
            };
        } 
        
        return new ResultViewModel<RoomDto>
        {
            IsSuccess = true,
            Data = roomDto,
            Message = "Request success"
        };
    }
    
    [HttpPut("{id}")]
    public async Task<ResultViewModel<RoomDto>> UpdateRoom([FromRoute]int id, [FromBody] CreateUpdateRoomDto createUpdateRoomDto)
    {
        var roomDto = await _roomService.UpdateRoom(id, createUpdateRoomDto);
        if (roomDto is null)
        {
            return new ResultViewModel<RoomDto>
            {
                IsSuccess = false,
                Data = null,
                Message = "Bad request"
            };
        }

        return new ResultViewModel<RoomDto>
        {
            IsSuccess = true,
            Data = roomDto,
            Message = "Request success"
        };
    }

    [HttpDelete("{id}")]
    public async Task<ResultViewModel<RoomDto>> DeleteFacility(int id)
    {
        var result = await _roomService.DeleteRoom(id);
        if (!result)
        {
            return new ResultViewModel<RoomDto>
            {
                IsSuccess = false,
                Message = "delete fail , object not found"
            };
        }
        return new ResultViewModel<RoomDto>
        {
            IsSuccess = true,
            Message = "delete success"
        };
    }
}
