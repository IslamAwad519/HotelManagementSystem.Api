using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Api.Models;

public class Customer : Person
{
    [MaxLength(15)]
    public string PassportNumber { get; set; }

    public ICollection<Reservation> Reservations { get; set; }
}
