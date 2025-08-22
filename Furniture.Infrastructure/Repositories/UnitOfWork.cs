using Furniture.Application.Interfaces.Repositories;
using Furniture.Infrastructure.Persistence;

namespace Furniture.Infrastructure.Repositories;

public class UnitOfWork: IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveAsync(CancellationToken ct = default)
    {
        return await _context.SaveChangesAsync(ct);
    }
}