using HotelManagementSystem.Api.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Api.Models;

public class Offer :BaseModel
{
    [MaxLength(100)]
    public string OfferName { get; set; }
    [MaxLength(200)]
    public string Description { get; set; } 
    public double DiscountPercentage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int RoomTypeId { get; set; }


    public RoomType RoomType { get; set; }
}
