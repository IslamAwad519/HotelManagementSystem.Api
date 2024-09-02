using HotelManagementSystem.Api.DTOs.Auth;
using HotelManagementSystem.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace HotelManagementSystem.Api.Services.Authentication;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtService _jwtService;
    public AuthService(UserManager<ApplicationUser> userManager, IJwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async Task<AuthResponse?> Login(string email, string password)
    {
        // check user
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return null;
        }

        //check password
        var isValidPassword = await _userManager.CheckPasswordAsync(user, password);
        if (!isValidPassword)
        {
            return null;
        }

        var role = await _userManager.GetRolesAsync(user);
        //user.Roles = roles;

        //generate jet token
        var token =  await _jwtService.GenerateToken(user);

        // return response 
        return new AuthResponse(user.Id, user.FirstName,user.LastName, user.Email!, token);

    }
    public async Task<AuthResponse?> Register(RegisterDto registerDto)
    {
        var user = new ApplicationUser
        {
            Email = registerDto.Email,
            UserName = registerDto.Email,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (result.Succeeded)
        {
            // Validate and assign the selected role
            var validRoles = new[] { "Admin", "Customer", "Receptionist" };
            var roleToAssign = validRoles.Contains(registerDto.Role) ? registerDto.Role : "Customer";
            await _userManager.AddToRoleAsync(user, roleToAssign);

            var token = await _jwtService.GenerateToken(user);

            
            return new AuthResponse(user.Id,user.FirstName,user.LastName, user.Email, token);
        }

        return null;
    }

    //public async Task<ResponseDto> RegisterAsync(RegisterDto registerDto)
    //{

    //    var user = new ApplicationUser
    //    {
    //        Email = registerDto.Email,
    //        UserName = registerDto.Email,
    //        FirstName = registerDto.FirstName,
    //        LastName = registerDto.LastName,
    //    };

    //   var result =  await _userManager.CreateAsync(user,registerDto.Password);
    //    if(result.Succeeded)
    //    {
    //        // Add the user to the role by name
    //        await _userManager.AddToRoleAsync(user,DefaultRoles.Student);

    //        //generate jet token
    //        var (token, expiresIn) = _jwtProvider.GenerateToken(user);

    //        // return response 
    //        return new ResponseDto(user.Id, user.Email, token, expiresIn);
    //    }
    //    return null;
    //}

}
