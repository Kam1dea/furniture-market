using Furniture.Domain.Entities;

namespace Furniture.Application.Interfaces.Repositories;

public interface IReviewRepository
{
    Task<Review?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<Review>> GetAllAsync(CancellationToken ct = default);
    Task<IEnumerable<Review>> GetByProductIdAsync(int productId, CancellationToken ct = default);
    Task<IEnumerable<Review>> GetByUserIdAsync(string userId, CancellationToken ct = default);

    Task AddAsync(Review review, CancellationToken ct = default);
    Task UpdateAsync(Review review, CancellationToken ct = default);
    Task DeleteAsync(Review review, CancellationToken ct = default);
    Task<bool> ExistsByUserAndProductAsync(string userId, int productId, CancellationToken ct = default);
}