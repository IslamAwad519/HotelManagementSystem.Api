using HotelManagementSystem.Api.Models.Common;

namespace HotelManagementSystem.Api.Models;

public class Facility :BaseModel
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    public ICollection<RoomFacility>? Rooms { get; set; }
    public ICollection<ReservationFacility> Reservations { get; set; }
}
