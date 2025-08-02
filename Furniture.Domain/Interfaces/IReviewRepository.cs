using Furniture.Domain.Entities;

namespace Furniture.Domain.Interfaces;

public interface IReviewRepository
{
    Task<IEnumerable<Review>> GetAllAsync();
    Task<Review?> GetByIdAsync(int id);
    Task<Review?> CreateAsync(Review review);
    Task<Review?> UpdateAsync(int id, Review review);
    Task<bool> DeleteAsync(int id);
}