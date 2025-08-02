using System.ComponentModel.DataAnnotations;

namespace Furniture.Application.Dtos.Review;

public class CreateReviewDto
{
    [Required]
    public int Rating { get; set; }
    public string? Tittle { get; set; }
    [Required]
    public string? Content { get; set; }
    public int? ProductId { get; set; }
    public int? WorkerProfileId { get; set; }
    public string? UserId { get; set; }
    //public ICollection<ReviewImage>? ReviewImages { get; set; }
}