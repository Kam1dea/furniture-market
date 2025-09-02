using Furniture.Application.Interfaces.Repositories;
using Furniture.Domain.Entities;
using Furniture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Furniture.Infrastructure.Repositories;

public class WorkerProfileRepository: IWorkerProfileRepository
{
    private readonly ApplicationDbContext  _context;

    public WorkerProfileRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<WorkerProfile>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.WorkerProfiles
            .Include(wp => wp.Worker)
            .Include(wp => wp.Products)
            .Include(wp => wp.Reviews)
            .ThenInclude(r => r.User)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<WorkerProfile?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.WorkerProfiles
            .Include(wp => wp.Worker)
            .Include(wp => wp.Products)
            .Include(wp => wp.Reviews)
            .ThenInclude(r => r.User)
            .AsNoTracking()
            .FirstOrDefaultAsync(wp => wp.Id == id, ct);
            
    }

    public async Task<WorkerProfile?> GetByWorkerIdAsync(string workerId, CancellationToken ct = default)
    {
        return await _context.WorkerProfiles
            .Include(wp => wp.Worker)
            .Include(wp => wp.Products)
            .Include(wp => wp.Reviews)
            .ThenInclude(r => r.User)
            .AsNoTracking()
            .FirstOrDefaultAsync(wp => wp.WorkerId == workerId, ct);
    }

    public async Task AddAsync(WorkerProfile workerProfile, CancellationToken ct = default)
    {
        await _context.WorkerProfiles.AddAsync(workerProfile, ct);
    }

    public async Task UpdateAsync(WorkerProfile workerProfile, CancellationToken ct = default)
    {
        _context.WorkerProfiles.Update(workerProfile);
    }

    public async Task<bool> IsExistingAsync(string workerId, CancellationToken ct = default)
    {
        var existing = await _context.WorkerProfiles
            .Where(wp => wp.WorkerId == workerId)
            .AnyAsync(ct);
        
        return existing;
    }
}