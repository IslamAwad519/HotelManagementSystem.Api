using HotelManagementSystem.Api.Models.Common;

namespace HotelManagementSystem.Api.Models;

public class ReservationRoomFacility :BaseModel
{
    public int ReservationId { get; set; }
    public int RoomId { get; set; }
    public int FacilityId { get; set; }

    public Room Room { get; set; }
    public Facility Facility { get; set; }
    public Reservation Reservation { get; set; }
}
