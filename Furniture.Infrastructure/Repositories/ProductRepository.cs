using Furniture.Domain.Entities;
using Furniture.Domain.Interfaces;
using Furniture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Furniture.Infrastructure.Repositories;

public class ProductRepository: IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Domain.Entities.Product>> GetAllAsync()
    {
        var products = _context.Products.AsNoTracking().ToListAsync();
        
        return await products;
    }
}