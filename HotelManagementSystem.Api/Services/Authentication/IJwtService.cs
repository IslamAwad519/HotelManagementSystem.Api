using HotelManagementSystem.Api.Models;

namespace HotelManagementSystem.Api.Services.Authentication;

public interface IJwtService
{
    Task<string>  GenerateToken(ApplicationUser user);
}
