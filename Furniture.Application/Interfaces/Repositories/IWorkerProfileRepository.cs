using Furniture.Domain.Entities;

namespace Furniture.Application.Interfaces.Repositories;

public interface IWorkerProfileRepository
{
    Task <IEnumerable<WorkerProfile>> GetAllAsync(CancellationToken ct = default);
    Task<WorkerProfile?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<WorkerProfile?> GetByWorkerIdAsync(string workerId, CancellationToken ct = default);
    Task AddAsync(WorkerProfile workerProfile, CancellationToken ct = default);
    Task UpdateAsync(WorkerProfile workerProfile, CancellationToken ct = default);
    Task<bool> IsExistingAsync(string workerId, CancellationToken ct = default);
}