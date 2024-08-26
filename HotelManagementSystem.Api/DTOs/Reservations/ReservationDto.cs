using HotelManagementSystem.Api.DTOs.ReservationRoomFacilities;
using HotelManagementSystem.Api.DTOs.ReservationRooms;
using HotelManagementSystem.Api.Models.Enums;

namespace HotelManagementSystem.Api.DTOs.Reservations;

public class ReservationDto
{
    public int Id { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public decimal TotalAmount { get; set; }
    public ReservationStatus ReservationStatus { get; set; }
    public int NumberAdults { get; set; }
    public int? NumberChildren { get; set; }
    public int CustomerId { get; set; }
    public string PaymentIntentId { get; set; }
    public ICollection<ReservationRoomFacilityDto> RoomFacilities { get; set; }
    public ICollection<ReservationRoomDto> Rooms { get; set; }
}
