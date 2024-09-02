using HotelManagementSystem.Api.DTOs.Auth;
using HotelManagementSystem.Api.Services.Authentication;
using HotelManagementSystem.Api.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ResultViewModel<AuthResponse?>> Login(LoginDto request)
    {
        var result = await _authService.Login(request.Email, request.Password);

        if (result is null)
        {
            return new ResultViewModel<AuthResponse?>
            {
                IsSuccess = false,
                Data = null,
                Message = "Invalid email/password"
            };
        }
        return new ResultViewModel<AuthResponse?>
        {
            IsSuccess = false,
            Data = result,
            Message = "login success"
        };
    }
    
    [HttpPost("register")]
    public async Task<ResultViewModel<AuthResponse?>> Register(RegisterDto request)
    {
        var result = await _authService.Register(request);

        if (result is null)
        {
            return new ResultViewModel<AuthResponse?>
            {
                IsSuccess = false,
                Data = null,
                Message = "Invalid email/password"
            };
        }
        return new ResultViewModel<AuthResponse?>
        {
            IsSuccess = false,
            Data = result,
            Message = "register success"
        };
    }

    //[HttpPost("add-instructor")]
    //public async Task<IActionResult> RegisterInstructor(RegisterDto request)
    //{
    //    var result = await _authService.RegisterAsync(request);

    //    return result is null
    //        ? BadRequest("Invalid email/password")
    //        : Ok(result);
    //}
}
