using Furniture.Domain.Entities;

namespace Furniture.Domain.Interfaces;

public interface IWorkerProfileRepository
{
    Task <IEnumerable<WorkerProfile>> GetAllAsync();
    Task<WorkerProfile?> GetByIdAsync(int id);
    //Task<WorkerProfile> GetOwnAsync();
    Task<WorkerProfile> CreateAsync(WorkerProfile workerProfile);
    Task<WorkerProfile?> UpdateAsync(string id, WorkerProfile workerProfile);
    Task<bool> IsExistingAsync(string id);
}