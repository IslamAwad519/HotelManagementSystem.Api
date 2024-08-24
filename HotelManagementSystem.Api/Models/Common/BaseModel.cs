namespace HotelManagementSystem.Api.Models.Common;

public abstract class BaseModel
{
    public int Id { get; set; }
    public bool Deleted { get; set; }
}
