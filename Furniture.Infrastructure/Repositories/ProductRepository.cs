using Furniture.Application.Interfaces.Repositories;
using Furniture.Domain.Entities;
using Furniture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Furniture.Infrastructure.Repositories;

public class ProductRepository: IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        var products = _context.Products.AsNoTracking().ToListAsync();
        
        return await products;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        var product  = _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        
        return await product;
    }

    public async Task<Product?> CreateAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> UpdateAsync(int id, Product product)
    {
        var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (existingProduct == null)
        {
            return null;
        }
        
        _context.Entry(existingProduct).CurrentValues.SetValues(product);
        await _context.SaveChangesAsync();
        
        return existingProduct;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existingProduct = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

        if (existingProduct == null)
        {
            return false;
        }
        
        _context.Products.Remove(existingProduct);
        await _context.SaveChangesAsync();
        return true;
    }
}