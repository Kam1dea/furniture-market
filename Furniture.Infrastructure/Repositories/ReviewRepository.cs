using AutoMapper;
using Furniture.Domain.Entities;
using Furniture.Domain.Interfaces;
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

    public async Task<IEnumerable<Review>> GetAllAsync()
    {
        var reviews = await _context.Reviews.AsNoTracking().ToListAsync();

        return reviews;
    }

    public async Task<Review?> GetByIdAsync(int id)
    {
        var review = await _context.Reviews.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        return review;
    }

    public async Task<Review?> CreateAsync(Review review)
    {
        await _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();
        return review;
    }

    public async Task<Review?> UpdateAsync(int id, Review review)
    {
        var existingReview = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);

        if (existingReview == null)
        {
            return null;
        }
        
        _context.Entry(existingReview).CurrentValues.SetValues(review);
        await _context.SaveChangesAsync();
        
        return existingReview;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existingReview = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);

        if (existingReview == null)
        {
            return false;
        }
        
        _context.Reviews.Remove(existingReview);
        await _context.SaveChangesAsync();
        return true;
    }
}