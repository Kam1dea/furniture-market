namespace Furniture.Application.Dtos.Product;

public class UpdateProductDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Material { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Depth { get; set; }
}