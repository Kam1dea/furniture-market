using System.Security.Claims;
using AutoMapper;
using Furniture.Application.Dtos.WorkerProfile;
using Furniture.Application.Exceptions;
using Furniture.Domain.Entities;
using Furniture.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Furniture.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class WorkerProfileController: ControllerBase
{
    private readonly IWorkerProfileRepository _workerProfileRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public WorkerProfileController(IWorkerProfileRepository workerProfileRepository, IMapper mapper, UserManager<User> userManager)
    {
        _workerProfileRepository = workerProfileRepository;
        _mapper = mapper;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var workerProfiles = await _workerProfileRepository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<WorkerProfileDto>>(workerProfiles));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute]int id)
    {
        var workerProfile = await _workerProfileRepository.GetByIdAsync(id);
        
        if (workerProfile == null)
            throw new NotFoundException($"WorkerProfile with ID {id} not found");
        
        return Ok(_mapper.Map<WorkerProfileDto>(workerProfile));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody]CreateWorkerProfileDto workerProfileDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (_workerProfileRepository.IsExistingAsync(userId!).Result)
            return BadRequest("You already have profile");
        
        var workerProfile = _mapper.Map<WorkerProfile>(workerProfileDto);
        workerProfile.WorkerId = userId;
        
        await _workerProfileRepository.CreateAsync(workerProfile);
        return Ok(_mapper.Map<WorkerProfileDto>(workerProfile));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateWorkerProfileDto workerProfileDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!_workerProfileRepository.IsExistingAsync(userId!).Result)
            return BadRequest("You don't have profile");
        
        var workerProfileEntity = _mapper.Map<WorkerProfile>(workerProfileDto);
        await _workerProfileRepository.UpdateAsync(userId!, workerProfileEntity);
        
        return Ok(_mapper.Map<WorkerProfileDto>(workerProfileEntity));
    }
}