using Microsoft.AspNetCore.Http;

namespace Furniture.Application.Dtos.Product;

public class CreateProductWithImageDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string Material { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int Depth { get; set; }

    public List<IFormFile> Images { get; set; } = new();
    
}