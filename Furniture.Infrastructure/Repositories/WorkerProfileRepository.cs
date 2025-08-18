using Furniture.Domain.Entities;
using Furniture.Domain.Interfaces;
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

    public async Task<IEnumerable<WorkerProfile>> GetAllAsync()
    {
        var workerProfiles = await _context.WorkerProfiles.AsNoTracking().ToListAsync();
        
        return workerProfiles;
    }

    public async Task<WorkerProfile?> GetByIdAsync(int id)
    {
        var workerProfile = await _context.WorkerProfiles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        
        return workerProfile;
    }

    // public async Task<WorkerProfile> GetOwnAsync(string id)
    // {
    //     
    // }

    public async Task<WorkerProfile> CreateAsync(WorkerProfile workerProfile)
    {
        await _context.WorkerProfiles.AddAsync(workerProfile);
        await _context.SaveChangesAsync();
        return workerProfile;
    }

    public async Task<WorkerProfile?> UpdateAsync(string id, WorkerProfile workerProfile)
    {
        var existingWorkerProfile = await _context.WorkerProfiles.FirstOrDefaultAsync(p => p.Id == workerProfile.Id);
        
        if (existingWorkerProfile == null)
            return null;
        
        _context.Entry(existingWorkerProfile).CurrentValues.SetValues(workerProfile);
        await _context.SaveChangesAsync();
        
        return workerProfile;
    }

    public async Task<bool> IsExistingAsync(string id)
    {
        if (await _context.WorkerProfiles.AnyAsync(p => p.WorkerId == id))
            return true;
        return false;
    }
}