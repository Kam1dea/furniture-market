using Furniture.Application.Dtos.Product;
using Furniture.Application.Dtos.Review;

namespace Furniture.Application.Dtos.WorkerProfile;

public class WorkerProfileDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public double Rating { get; set; }
    public int TotalReviews { get; set; }
    public IEnumerable<ProductDto> Products { get; set; } = new List<ProductDto>();
    public IEnumerable<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();
}