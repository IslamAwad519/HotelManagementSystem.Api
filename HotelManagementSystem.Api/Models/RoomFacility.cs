using HotelManagementSystem.Api.Models.Common;

namespace HotelManagementSystem.Api.Models;

public class RoomFacility :BaseModel
{
    public int RoomId { get; set; }
    public int FacilityId { get; set; }

    public Room Room { get; set; }
    public Facility Facility { get; set; }

    public ICollection<ReservationRoomFacility> Reservations { get; set; }
}
