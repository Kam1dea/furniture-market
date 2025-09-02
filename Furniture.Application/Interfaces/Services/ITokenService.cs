using System.Security.Claims;
using Furniture.Domain.Entities;

namespace Furniture.Application.Interfaces.Services;

public interface ITokenService
{
    string GenerateJwtToken(User user, IEnumerable<string> roles);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}