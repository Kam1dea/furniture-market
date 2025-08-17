namespace Furniture.Domain.Entities;

public class WorkerProfile
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public double Rating { get; set; }
    public string? WorkerId { get; set; }
    public User? Worker { get; set; }
    
    public ICollection<Product> Products { get; set; } = new List<Product>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}