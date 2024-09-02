using HotelManagementSystem.Api.DTOs.Auth;

namespace HotelManagementSystem.Api.Services.Authentication;

public interface IAuthService
{
    Task<AuthResponse?> Login(string email, string password);
    Task<AuthResponse?> Register(RegisterDto registerDto);
}
