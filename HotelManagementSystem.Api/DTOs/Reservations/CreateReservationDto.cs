using HotelManagementSystem.Api.Models.Enums;
using HotelManagementSystem.Api.Models;

namespace HotelManagementSystem.Api.DTOs.Reservations;

public class CreateReservationDto
{
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public ReservationStatus ReservationStatus { get; set; } 
    public int NumberAdults { get; set; }
    public int? NumberChildren { get; set; }
    public int CustomerId { get; set; }

    public Dictionary<int, IEnumerable<int>> RoomFacilities { get; set; }
}
   
