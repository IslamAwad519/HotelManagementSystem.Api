using HotelManagementSystem.Api.Models.Enums;

namespace HotelManagementSystem.Api.DTOs.Rooms;

public class CreateUpdateRoomDto
{
    public int RoomNumber { get; set; }
    public int Floor { get; set; }
    public Occupancy Occupancy { get; set; }
    public bool Status { get; set; } 
    public int RoomTypeId { get; set; }

    public ICollection<int> Facilities { get; set; }
}
