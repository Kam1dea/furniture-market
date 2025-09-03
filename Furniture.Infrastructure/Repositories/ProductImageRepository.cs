using Furniture.Application.Interfaces.Repositories;
using Furniture.Domain.Entities;
using Furniture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Furniture.Infrastructure.Repositories;

public class ProductImageRepository:  IProductImageRepository
{
    private readonly ApplicationDbContext _context;

    public ProductImageRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddRangeAsync(IEnumerable<ProductImage> images, CancellationToken ct = default)
    {
        await _context.ProductImages.AddRangeAsync(images, ct);
    }

    public async Task DeleteRangeAsync(IEnumerable<ProductImage> images, CancellationToken ct = default)
    {
        _context.ProductImages.RemoveRange(images);
    }

    public async Task<IEnumerable<ProductImage>> GetByProductId(int productId, CancellationToken ct = default)
    {
        return await _context.ProductImages
            .Where(p => p.ProductId == productId)
            .ToListAsync(ct);
    }
}