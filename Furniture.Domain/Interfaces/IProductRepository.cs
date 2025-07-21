using Furniture.Domain.Entities;

namespace Furniture.Domain.Interfaces;

public interface IProductRepository
{
    Task<IReadOnlyList<Entities.Product>> GetAllAsync();
}