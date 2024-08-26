using HotelManagementSystem.Api.DTOs.Reservations;
using HotelManagementSystem.Api.DTOs.Rooms;
using HotelManagementSystem.Api.Models;

namespace HotelManagementSystem.Api.Services.Reservations;

public interface IReservationService
{
    Task<List<RoomDto>> GetAvailableRooms();
    Task<IEnumerable<ReservationDto>> GetAllReservation(); 
    Task<ReservationDto?> GetReservation(int reservationId);
    Task<ReservationDto?> AddReservation(CreateReservationDto createReservationDto);

}
