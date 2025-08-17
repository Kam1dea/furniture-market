using Furniture.Domain.Entities;

namespace Furniture.Infrastructure.Services;

public interface ITokenService
{
    string GenerateJwtToken(User user, IEnumerable<string> roles);
    string GenerateRefreshToken();
}