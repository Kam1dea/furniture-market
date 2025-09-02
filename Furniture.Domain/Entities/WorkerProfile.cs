namespace Furniture.Domain.Entities;

public class WorkerProfile
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public double Rating { get; set; }
    public int TotalReviews { get; set; }
    
    public string? WorkerId { get; set; }
    public User? Worker { get; set; }
    
    public ICollection<Product> Products { get; set; } = new List<Product>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}