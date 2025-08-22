namespace Furniture.Application.Interfaces.Services;

public interface ICurrentUserService
{
    string? UserId { get; }
    bool IsAuthenticated { get; }
    string? Email { get; }
}