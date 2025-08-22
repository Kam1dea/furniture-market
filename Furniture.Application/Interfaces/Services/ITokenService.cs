using Furniture.Domain.Entities;

namespace Furniture.Application.Interfaces.Services;

public interface ITokenService
{
    string GenerateJwtToken(User user, IEnumerable<string> roles);
    string GenerateRefreshToken();
}