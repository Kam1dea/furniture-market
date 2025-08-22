namespace Furniture.Domain.Entities;

public class Review
{
    public int Id { get; set; }
    public int Rating { get; set; }
    public string Tittle { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }

    public string? UserId { get; set; }
    public User? User { get; set; }

    public int? ProductId { get; set; }
    public Product? Product { get; set; }

    public ICollection<ReviewImage> ReviewImages { get; set; } =  new List<ReviewImage>();
}