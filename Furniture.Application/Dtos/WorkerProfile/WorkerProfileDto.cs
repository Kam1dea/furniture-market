namespace Furniture.Application.Dtos.WorkerProfile;

public class WorkerProfileDto
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public double Rating { get; set; }
    public string? WorkerId { get; set; }
    public ICollection<Domain.Entities.Product> Products { get; set; } = new List<Domain.Entities.Product>();
    public ICollection<Domain.Entities.Review> Reviews { get; set; } = new List<Domain.Entities.Review>();
}