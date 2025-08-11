namespace Furniture.Application.Dtos;

public class ErrorResponseDto
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public List<string> Errors { get; set; } = new();

    public static ErrorResponseDto FromException(Exception ex, int statusCode)
    {
        return new ErrorResponseDto
        {
            StatusCode = statusCode,
            Message = "An unexpected error occurred.",
            Errors = new List<string> { ex.Message }
        };
    }
}