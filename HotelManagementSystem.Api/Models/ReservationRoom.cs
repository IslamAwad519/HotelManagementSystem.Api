using HotelManagementSystem.Api.Models.Common;

namespace HotelManagementSystem.Api.Models;

public class ReservationRoom :BaseModel
{
    public int RoomId { get; set; }
    public int ReservationId { get; set; }

    public Room Room { get; set; }
    public Reservation Reservation { get; set; }
}
