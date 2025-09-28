using System.Security.Claims;
using Furniture.Application.Dtos.Account;
using Furniture.Application.Interfaces.Services;
using Furniture.Application.Services;
using Furniture.Domain.Entities;
using Furniture.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Furniture.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly JwtSettings _jwtSettings;


    public AuthController(
        UserManager<User> userManager,
        ITokenService tokenService,
        IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _jwtSettings = jwtSettings.Value;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = new User
        {
            UserName = dto.Email,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        var role = dto.UserRole?.ToLower() switch
        {
            "admin" => UserRole.Admin,
            "worker" => UserRole.Worker,
            _ => UserRole.User
        };

        var roleName = role.ToString();

        // Создаём роль, если её нет
        if (!await _userManager.IsInRoleAsync(user, roleName))
            await _userManager.AddToRoleAsync(user, roleName);

        // Назначаем роль
        await _userManager.AddToRoleAsync(user, roleName);
        
        // Перечитываем пользователя
        user = await _userManager.FindByIdAsync(user.Id); // или FindByEmailAsync
        var roles = await _userManager.GetRolesAsync(user); // теперь роли будут
        Console.WriteLine($"Roles: {string.Join(", ", roles)}"); //  Проверь вывод

        return Ok(new { Message = "User registered successfully" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            return Unauthorized(new AuthResponseDto
            {
                IsSuccess = false,
                Errors = new List<string> { "Invalid email or password" }
            });

        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenService.GenerateJwtToken(user, roles);
        var refreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);
        await _userManager.UpdateAsync(user);
        

        return Ok(new AuthResponseDto
        {
            Token = token,
            RefreshToken = refreshToken,
            IsSuccess = true
        });
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<IActionResult> Refresh(RefreshTokenDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var principal = _tokenService.GetPrincipalFromExpiredToken(dto.Token);
        if (principal == null)
            return BadRequest("Invalid access token");
        
        var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            return BadRequest("Invalid access token");
        
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null || user.RefreshToken != dto.RefreshToken ||  user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            return BadRequest("Invalid refresh token");
        
        var roles = await _userManager.GetRolesAsync(user);
        var newToken = _tokenService.GenerateJwtToken(user, roles);
        var newRefreshToken = _tokenService.GenerateRefreshToken();
        user.RefreshToken = newRefreshToken;
        await _userManager.UpdateAsync(user);

        return Ok(new AuthResponseDto
        {
            Token = newToken,
            RefreshToken = newRefreshToken,
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