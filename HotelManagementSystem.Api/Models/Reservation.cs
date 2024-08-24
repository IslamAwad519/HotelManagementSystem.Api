using HotelManagementSystem.Api.Models.Common;
using HotelManagementSystem.Api.Models.Enums;

namespace HotelManagementSystem.Api.Models;

public class Reservation : BaseModel
{
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public decimal TotalAmount { get; set; }
    public ReservationStatus ReservationStatus { get; set; }
    public int NumberAdults { get; set; }
    public int? NumberChildren { get; set; }
    public int RoomId { get; set; }
    // public int StaffId { get; set; }
    public int CustomerId { get; set; }


    //public Staff Staff { get; set; }
    public Room? Room { get; set; }
    public Customer Customer { get; set; }
    public ICollection<ReservationRoom> Rooms { get; set; }
    public ICollection<ReservationFacility> Facilities { get; set; }

}
