using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Furniture.Application.Dtos.Account;
using Furniture.Application.Services;
using Furniture.Domain.Entities;
using Furniture.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Furniture.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ITokenService _tokenService;

    public AuthController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
        IConfiguration configuration, ITokenService tokenService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var user = new User
        {
            UserName = dto.Email,
            Email = dto.Email,
            FirstName = dto.FirstName ?? string.Empty,
            LastName = dto.LastName ?? string.Empty
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        var role = dto.UserRole switch
        {
            "Admin" => "Admin",
            "Worker" => "Worker",
            _ => "User"
        };

        if (!await _roleManager.RoleExistsAsync(role))
            await _roleManager.CreateAsync(new IdentityRole(role));

        await _userManager.AddToRoleAsync(user, role);

        return Ok(new { Message = "User registered successfully" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            return Unauthorized(new AuthResponseDto
                { IsSuccess = false, Errors = new[] { "Invalid email or password" } });

        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenService.GenerateJwtToken(user, roles);
        var refreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _userManager.UpdateAsync(user);

        return Ok(new AuthResponseDto
        {
            Token = token,
            RefreshToken = refreshToken,
            IsSuccess = true
        });
    }
    
    [Authorize]
    [HttpGet("me")]
    public IActionResult GetCurrentUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var email = User.FindFirstValue(ClaimTypes.Email);
        var name = User.FindFirstValue(ClaimTypes.Name);
        var roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value);

        return Ok(new { userId, email, name, roles });
    }
}