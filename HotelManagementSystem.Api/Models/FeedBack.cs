using HotelManagementSystem.Api.Models.Common;

namespace HotelManagementSystem.Api.Models;

public class FeedBack :BaseModel
{
    public int Rating { get; set; }
    public string Comment { get; set; }
    public int CustomerId { get; set; }
    public int ReservationId { get; set; }

    public Customer Customer { get; set; }
    public Reservation Reservation { get; set; }
}
