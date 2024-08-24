using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Api.DTOs.Offers;

public class CreateUpdateOfferDto
{
    [MaxLength(100)]
    public string OfferName { get; set; }
    [MaxLength(200)]
    public string Description { get; set; }
    public double DiscountPercentage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int RoomTypeId { get; set; }
}
