using Furniture.Domain.Entities;

namespace Furniture.Application.Interfaces.Repositories;

public interface IProductImageRepository
{
    Task AddRangeAsync(IEnumerable<ProductImage> images, CancellationToken ct = default);
    Task DeleteRangeAsync(IEnumerable<ProductImage> images, CancellationToken ct = default);
    Task <IEnumerable<ProductImage>> GetByProductId(int productId, CancellationToken ct = default);
}