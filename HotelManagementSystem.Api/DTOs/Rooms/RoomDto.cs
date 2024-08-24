using HotelManagementSystem.Api.DTOs.Images;
using HotelManagementSystem.Api.DTOs.RoomFacilities;
using HotelManagementSystem.Api.Models.Enums;

namespace HotelManagementSystem.Api.DTOs.Rooms;

public class RoomDto
{
    public int RoomNumber { get; set; }
    public int Floor { get; set; }
    public Occupancy Occupancy { get; set; }
    public bool Status { get; set; } 

    public int RoomTypeId { get; set; }

    public ICollection<ImageDto> Images { get; set; }
    public ICollection<RoomFacilityDto> Facilities { get; set; }
}
