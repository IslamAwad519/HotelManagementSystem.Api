namespace HotelManagementSystem.Api.DTOs.Auth;

public record AuthResponse(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    string Token
);