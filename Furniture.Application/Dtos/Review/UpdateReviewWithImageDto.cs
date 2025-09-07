using Microsoft.AspNetCore.Http;

namespace Furniture.Application.Dtos.Review;

public class UpdateReviewWithImageDto
{
    public int Rating { get; set; }
    public string Tittle { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public List<IFormFile> NewImages { get; set; } = new();
}