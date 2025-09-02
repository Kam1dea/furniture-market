using Furniture.Domain.Entities;

namespace Furniture.Application.Interfaces.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<Product>> GetAllAsync(CancellationToken ct = default);
    Task<IEnumerable<Product>> GetByWorkerProfileIdAsync(int workerProfileId, CancellationToken ct = default);
    Task AddAsync(Product product, CancellationToken ct = default);
    Task UpdateAsync(Product product, CancellationToken ct = default);
    Task DeleteAsync(Product product, CancellationToken ct = default);
}