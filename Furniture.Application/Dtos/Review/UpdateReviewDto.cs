namespace Furniture.Application.Dtos.Review;

public class UpdateReviewDto
{
    public int Rating { get; set; }
    public string Tittle { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    //public ICollection<ReviewImage>? ReviewImages { get; set; }
}