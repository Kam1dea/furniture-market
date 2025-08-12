namespace Furniture.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? Color { get; set; }
    public string? Material { get; set; }
    public decimal Price { get; set; }

    public double Width { get; set; }
    public double Height { get; set; }
    public double Depth { get; set; }

    public string WorkerProfileId { get; set; }
    public WorkerProfile? WorkerProfile { get; set; }

    public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}