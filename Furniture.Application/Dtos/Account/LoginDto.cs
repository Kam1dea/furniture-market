using System.ComponentModel.DataAnnotations;

namespace Furniture.Application.Dtos.Account;

public class LoginDto
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
}