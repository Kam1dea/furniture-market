using Furniture.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Furniture.Domain.Entities;

public class User : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public UserRole UserRole { get; set; }
    
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public WorkerProfile? WorkerProfile { get; set; }
}