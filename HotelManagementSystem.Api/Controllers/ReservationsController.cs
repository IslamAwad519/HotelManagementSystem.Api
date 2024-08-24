using HotelManagementSystem.Api.DTOs.Rooms;
using HotelManagementSystem.Api.Services.Rooms;
using HotelManagementSystem.Api.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly IRoomService _roomService;

    public ReservationsController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet("available-Rooms")]
    public async Task<ResultViewModel<IEnumerable<RoomDto>>> GetAvailableRooms()
    {
        var roomsDto = await _roomService.GetAvailableRooms();
        return new ResultViewModel<IEnumerable<RoomDto>>
        {
            IsSuccess = true,
            Data = roomsDto,
            Message = "request success"
        };
    }
}
