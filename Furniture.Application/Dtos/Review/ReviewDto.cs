namespace Furniture.Application.Dtos.Review;

public class ReviewDto
{
    public int Id { get; set; }
    public int Rating { get; set; }
    public string? Tittle { get; set; }
    public string? Content { get; set; }
    public int? ProductId { get; set; }
    public int? WorkerProfileId { get; set; }
    public string? UserId { get; set; }
    //public ICollection<ReviewImage>? ReviewImages { get; set; }
}