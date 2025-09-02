namespace Furniture.Application.Dtos.Review;

public class ReviewDto
{
    public int Id { get; set; }
    public int Rating { get; set; }
    public string Tittle { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
    public string UserName { get; set; } = string.Empty;
    //public ICollection<ReviewImage>? ReviewImages { get; set; }
}