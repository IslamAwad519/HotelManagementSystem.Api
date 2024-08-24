using HotelManagementSystem.Api.Models.Enums;

namespace HotelManagementSystem.Api.Models;

public class Staff : Person
{
    public decimal Salary { get; set; }
    public JobRole JobRole { get; set; }

    public ICollection<Reservation> Reservations { get; set; }
}
