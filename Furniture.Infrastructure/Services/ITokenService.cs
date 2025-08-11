using System.Security.Claims;
using Furniture.Domain.Entities;

namespace Furniture.Application.Services;

public interface ITokenService
{
    string GenerateJwtToken(User user, IEnumerable<string> roles);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    bool IsRefreshTokenValid(string refreshToken, User user);
}