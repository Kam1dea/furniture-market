using Furniture.Application.Interfaces.Repositories;
using Furniture.Domain.Entities;
using Furniture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Furniture.Infrastructure.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly ApplicationDbContext _context;

    public ReviewRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<Review?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.Reviews
            .Include(r => r.User)
            .Include(r => r.Product)
            .Include(r => r.ReviewImages)
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id, ct);
    }

    public async Task<IEnumerable<Review>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Reviews
            .Include(r => r.User)
            .Include(r => r.Product)
            .Include(r => r.ReviewImages)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<Review>> GetByProductIdAsync(int productId, CancellationToken ct = default)
    {
        return await _context.Reviews
            .Where(r => r.ProductId == productId)
            .Include(r => r.User)
            .Include(r => r.ReviewImages)
            .OrderByDescending(r => r.CreatedOn)
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<Review>> GetByUserIdAsync(string userId, CancellationToken ct = default)
    {
        return await _context.Reviews
            .Where(r => r.UserId == userId)
            .Include(r => r.Product)
            .Include(r => r.ReviewImages)
            .OrderByDescending(r => r.CreatedOn)
            .ToListAsync(ct);
    }

    public async Task AddAsync(Review review, CancellationToken ct = default)
    {
        await _context.Reviews.AddAsync(review, ct);
    }

    public async Task UpdateAsync(Review review, CancellationToken ct = default)
    {
        _context.Reviews.Update(review);
    }

    public async Task DeleteAsync(Review review, CancellationToken ct = default)
    {
        _context.Reviews.Remove(review);
    }

    public async Task<bool> ExistsByUserAndProductAsync(string userId, int productId, CancellationToken ct = default)
    {
        return await _context.Reviews
            .AnyAsync(r => r.UserId == userId && r.ProductId == productId, ct);
    }
}