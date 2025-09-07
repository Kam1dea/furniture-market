using Furniture.Application.Interfaces.Repositories;
using Furniture.Domain.Entities;
using Furniture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Furniture.Infrastructure.Repositories;

public class ReviewImageRepository: IReviewImageRepository
{
    private readonly ApplicationDbContext _context;

    public ReviewImageRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddRangeAsync(IEnumerable<ReviewImage> images, CancellationToken ct = default)
    {
        await _context.ReviewImages.AddRangeAsync(images, ct);
    }

    public async Task DeleteRangeAsync(IEnumerable<ReviewImage> images, CancellationToken ct = default)
    {
        _context.ReviewImages.RemoveRange(images);
    }

    public async Task<IEnumerable<ReviewImage>> GetByReviewId(int reviewId, CancellationToken ct = default)
    {
        return await _context.ReviewImages
            .Where(r => r.ReviewId == reviewId)
            .ToListAsync(ct);
    }
}