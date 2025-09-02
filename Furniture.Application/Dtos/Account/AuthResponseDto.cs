namespace Furniture.Application.Dtos.Account;

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }
    public List<string>? Errors { get; set; } = new();
}