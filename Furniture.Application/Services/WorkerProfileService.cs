using AutoMapper;
using Furniture.Application.Dtos.WorkerProfile;
using Furniture.Application.Exceptions;
using Furniture.Application.Interfaces.Repositories;
using Furniture.Application.Interfaces.Services;
using Furniture.Domain.Entities;

namespace Furniture.Application.Services;

public class WorkerProfileService:  IWorkerProfileService
{
    private readonly IWorkerProfileRepository _workerProfileRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService  _currentUserService;

    public WorkerProfileService(IWorkerProfileRepository repository, IMapper mapper, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _workerProfileRepository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }


    public async Task<IEnumerable<WorkerProfileDto>> GetAllAsync(CancellationToken ct = default)
    {
        var profiles = await _workerProfileRepository.GetAllAsync(ct);
        
        return _mapper.Map<IEnumerable<WorkerProfileDto>>(profiles);
    }

    public async Task<WorkerProfileDto> GetMyProfileAsync(CancellationToken ct = default)
    {
        var workerId = _currentUserService.UserId
                       ?? throw new UnauthorizedAccessException("User is not authenticated");
        
        var profile = await _workerProfileRepository.GetByWorkerIdAsync(workerId, ct)
                      ?? throw new NotFoundException("Worker profile not found");
        
        return _mapper.Map<WorkerProfileDto>(profile);
    }

    public async Task<WorkerProfileDto> GetByWorkerIdAsync(int workerId, CancellationToken ct = default)
    {
        var profile = await _workerProfileRepository.GetByIdAsync(workerId, ct)
                      ?? throw new NotFoundException("Worker profile not found");
            
        return _mapper.Map<WorkerProfileDto>(profile);
    }

    public async Task<WorkerProfileDto> CreateProfileAsync(CreateWorkerProfileDto dto, CancellationToken ct = default)
    {
        var workerId = _currentUserService.UserId
                       ?? throw new UnauthorizedAccessException("User is not authenticated");

        var existing = await _workerProfileRepository.IsExistingAsync(workerId, ct);
        if (!existing)
            throw new InvalidOperationException("You already have a worker profile.");

        var profile = new WorkerProfile
        {
            WorkerId = workerId,
            Description = dto.Description,
            Location = dto.Location,
            Rating = 0,
            Name = _currentUserService.Name,
            Email = _currentUserService.Email,
        };
        
        await _workerProfileRepository.AddAsync(profile, ct);
        await _unitOfWork.SaveAsync(ct);
        
        return _mapper.Map<WorkerProfileDto>(profile);
    }

    public async Task<WorkerProfileDto> UpdateProfileAsync(UpdateWorkerProfileDto dto, CancellationToken ct = default)
    {
        var workerId = _currentUserService.UserId
                       ?? throw new UnauthorizedAccessException("User is not authenticated");
        
        var profile = await _workerProfileRepository.GetByWorkerIdAsync(workerId, ct)
                      ?? throw new NotFoundException("Worker profile not found");
        
        profile.Description = dto.Description;
        profile.Location = dto.Location;
        
        _workerProfileRepository.UpdateAsync(profile, ct);
        await _unitOfWork.SaveAsync(ct);
        
        return _mapper.Map<WorkerProfileDto>(profile);
    }
}