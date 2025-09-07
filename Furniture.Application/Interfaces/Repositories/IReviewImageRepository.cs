using Furniture.Domain.Entities;

namespace Furniture.Application.Interfaces.Repositories;

public interface IReviewImageRepository
{
    Task AddRangeAsync(IEnumerable<ReviewImage> images, CancellationToken ct = default);
    Task DeleteRangeAsync(IEnumerable<ReviewImage> images, CancellationToken ct = default);
    Task <IEnumerable<ReviewImage>> GetByReviewId(int reviewId, CancellationToken ct = default);
}