using Furniture.Application.Interfaces.Repositories;
using Furniture.Domain.Entities;
using Furniture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Furniture.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<Product?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.Products
            .Include(p => p.WorkerProfile!.Worker)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, ct);
    }

    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Products
            .Include(p => p.WorkerProfile!.Worker)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<Product>> GetByWorkerProfileIdAsync(int workerProfileId, CancellationToken ct = default)
    {
        return await _context.Products
            .Where(p => p.WorkerProfileId == workerProfileId)
            .Include(p => p.WorkerProfile.Worker)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task AddAsync(Product product, CancellationToken ct = default)
    {
        await _context.Products.AddAsync(product, ct);
    }

    public async Task UpdateAsync(Product product, CancellationToken ct = default)
    {
        _context.Products.Update(product);
    }

    public async Task DeleteAsync(Product product, CancellationToken ct = default)
    {
        _context.Products.Remove(product);
    }
}