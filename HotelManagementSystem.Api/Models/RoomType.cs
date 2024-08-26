using HotelManagementSystem.Api.Models.Common;

namespace HotelManagementSystem.Api.Models;

public class RoomType : BaseModel
{
    public string Type { get; set; }


    public ICollection<Room>? Rooms { get; set; }
    public Offer Offer { get; set; }
}
