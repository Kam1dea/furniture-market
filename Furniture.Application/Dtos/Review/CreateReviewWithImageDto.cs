using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Furniture.Application.Dtos.Review;

public class CreateReviewWithImageDto
{
    [Required]
    public int Rating { get; set; }
    public string Tittle { get; set; } = string.Empty;
    [Required]
    public string Content { get; set; } = string.Empty;
    public int ProductId { get; set; }
    public List<IFormFile> Images { get; set; } = new ();
}