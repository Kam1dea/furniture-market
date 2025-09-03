namespace Furniture.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string Material { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Depth { get; set; }
    public DateTime CreatedOn { get; set; } =  DateTime.UtcNow;
    public DateTime ModifiedOn { get; set; }
    public string WorkerName { get; set; } = string.Empty;

    public int WorkerProfileId { get; set; }
    public WorkerProfile? WorkerProfile { get; set; } 

    public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}