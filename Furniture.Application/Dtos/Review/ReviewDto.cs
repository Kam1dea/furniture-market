namespace Furniture.Application.Dtos.Review;

public class ReviewDto
{
    public int Id { get; set; }
    public int Rating { get; set; }
    public string? Tittle { get; set; }
    public string? Content { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
    public string? UserName { get; set; }
    //public ICollection<ReviewImage>? ReviewImages { get; set; }
}