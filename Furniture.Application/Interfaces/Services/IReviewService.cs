using Furniture.Application.Dtos.Review;

namespace Furniture.Application.Interfaces.Services;

public interface IReviewService
{
    Task<IEnumerable<ReviewDto>> GetAllAsync(CancellationToken ct = default);
    Task<ReviewDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<ReviewDto>> GetByProductAsync(int productId, CancellationToken ct = default);
    Task<IEnumerable<ReviewDto>> GetMyReviewsAsync(CancellationToken ct = default);

    Task<int> CreateReviewAsync(CreateReviewDto dto, CancellationToken ct = default);
    Task UpdateReviewAsync(int id, UpdateReviewDto dto, CancellationToken ct = default);
    Task DeleteReviewAsync(int id, CancellationToken ct = default);
}