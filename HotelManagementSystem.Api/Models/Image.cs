using System.ComponentModel.DataAnnotations;
using HotelManagementSystem.Api.Models.Common;

namespace HotelManagementSystem.Api.Models;

public class Image :BaseModel
{
    [MaxLength(250)]
    public string Name { get; set; }
    public string FilePath { get; set; }


    public int RoomId { get; set; }
    public Room? Room { get; set; }
}
