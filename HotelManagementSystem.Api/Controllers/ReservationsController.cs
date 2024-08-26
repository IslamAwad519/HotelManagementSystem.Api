using Microsoft.AspNetCore.Mvc;
using HotelManagementSystem.Api.DTOs.Reservations;
using HotelManagementSystem.Api.DTOs.Rooms;
using HotelManagementSystem.Api.Services.Reservations;
using HotelManagementSystem.Api.ViewModel;

namespace HotelManagementSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;
    public ReservationsController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet]
    public async Task<ResultViewModel<IEnumerable<ReservationDto>>> GetList()
    {
        var reservationDto = await _reservationService.GetAllReservation();

        return new ResultViewModel<IEnumerable<ReservationDto>>
        {
            IsSuccess = true,
            Data = reservationDto,
            Message = "request success"
        };
    }
   
    [HttpGet("{id}")]
    public async Task<ResultViewModel<ReservationDto>> GetFacility([FromRoute] int id)
    {
        var reservationDto = await _reservationService.GetReservation(id);

        if (reservationDto is null)
        {
            return new ResultViewModel<ReservationDto>
            {
                IsSuccess = false,
                Data = null,
                Message = "Bad request"
            };
        }


        return new ResultViewModel<ReservationDto>
        {
            IsSuccess = false,
            Data = reservationDto,
            Message = "Request success"
        };
    }
   
    [HttpGet("available-Rooms")]
    public async Task<ResultViewModel<IEnumerable<RoomDto>>> GetAvailableRooms()
    {
        var availableRooms = await _reservationService.GetAvailableRooms();
        return new ResultViewModel<IEnumerable<RoomDto>>
        {
            IsSuccess = true,
            Data = availableRooms,
            Message = "request success"
        };
    }

    [HttpPost("add-reservation")]
    public async Task<ResultViewModel<ReservationDto>> AddReservation(CreateReservationDto createReservationDto)
    {
        var reservationDto = await _reservationService.AddReservation(createReservationDto);
        if (reservationDto is null)
        {
            return new ResultViewModel<ReservationDto>
            {
                IsSuccess = false,
                Data = null,
                Message = "Room unavailable for booking"
            };
        }

        return new ResultViewModel<ReservationDto>
        {
            IsSuccess = true,
            Data = reservationDto,
            Message = "Request success"
        };
    }
}
