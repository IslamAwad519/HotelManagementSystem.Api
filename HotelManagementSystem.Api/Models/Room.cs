using HotelManagementSystem.Api.Models.Common;
using HotelManagementSystem.Api.Models.Enums;

namespace HotelManagementSystem.Api.Models;

public class Room: BaseModel
{
    public int RoomNumber { get; set; }
    public int Floor { get; set; }
    public Occupancy Occupancy { get; set; }
    public bool Status { get; set; } // حالة الغرةفة هل هي صالحة ولا تحت الصيانة
    public int RoomTypeId { get; set; }

    public RoomType RoomType { get; set; }
    public ICollection<RoomFacility> Facilities { get; set; }
    public ICollection<ReservationRoom> Reservations { get; set; }
    public ICollection<Image> Images { get; set; }
}
