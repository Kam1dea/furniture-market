namespace Furniture.Application.Dtos.Review;

public class UpdateReviewDto
{
    public int Rating { get; set; }
    public string? Tittle { get; set; }
    public string? Content { get; set; }
    //public ICollection<ReviewImage>? ReviewImages { get; set; }
}