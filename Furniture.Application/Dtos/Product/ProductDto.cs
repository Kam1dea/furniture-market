namespace Furniture.Application.Dtos.Product;

public class ProductDto
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
    public int WorkerProfileId { get; set; }
    //public ICollection<ProductImage>? ProductImages { get; set; }
}