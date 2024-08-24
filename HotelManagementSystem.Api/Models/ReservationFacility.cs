using HotelManagementSystem.Api.Models.Common;

namespace HotelManagementSystem.Api.Models;

public class ReservationFacility : BaseModel
{

    public int ReservationId { get; set; }
    public int FacilityId { get; set; }

    public Reservation Reservation { get; set; }
    public Facility Facility { get; set; }
}
