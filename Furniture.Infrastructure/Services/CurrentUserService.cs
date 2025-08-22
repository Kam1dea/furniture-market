using System.Security.Claims;
using Furniture.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace Furniture.Infrastructure.Services;

public class CurrentUserService: ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated == true;
    public string? Email =>  _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
}