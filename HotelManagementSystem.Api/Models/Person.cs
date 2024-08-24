using System.ComponentModel.DataAnnotations;
using HotelManagementSystem.Api.Models.Common;

namespace HotelManagementSystem.Api.Models;

public abstract class Person : BaseModel
{
    [MaxLength(100)]
    public string FirstName { get; set; }
    [MaxLength(100)]
    public string LastName { get; set; }
    [MaxLength(70)]
    public string Email { get; set; }
    [MaxLength(20)]
    public string Phone { get; set; }
    [MaxLength(50)]
    public string Address { get; set; }
    public DateTime DateOfBirth { get; set; }
}
