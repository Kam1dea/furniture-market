using Furniture.Application.Dtos.WorkerProfile;
using Furniture.Domain.Entities;

namespace Furniture.Application.Interfaces.Services;

public interface IWorkerProfileService
{
    Task<IEnumerable<WorkerProfileDto>> GetAllAsync(CancellationToken ct = default);
    Task<WorkerProfileDto> GetMyProfileAsync(CancellationToken ct = default);
    Task<WorkerProfileDto> GetByWorkerIdAsync(int workerId, CancellationToken ct = default);
    Task<WorkerProfileDto> CreateProfileAsync(CreateWorkerProfileDto dto, CancellationToken ct = default);
    Task<WorkerProfileDto> UpdateProfileAsync(UpdateWorkerProfileDto dto, CancellationToken ct = default);
}