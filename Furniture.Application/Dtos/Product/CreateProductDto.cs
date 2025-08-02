using System.ComponentModel.DataAnnotations;

namespace Furniture.Application.Dtos.Product;

public class CreateProductDto
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Description { get; set; }
    [Required]
    public string? Category { get; set; }
    public string? Color { get; set; }
    public string? Material { get; set; }
    [Required]
    public decimal Price { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Depth { get; set; }
    [Required]
    public int WorkerProfileId { get; set; }
    
}