using System.Security.Claims;
using AutoMapper;
using Furniture.Application.Dtos.WorkerProfile;
using Furniture.Application.Exceptions;
using Furniture.Application.Interfaces.Repositories;
using Furniture.Application.Interfaces.Services;
using Furniture.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Furniture.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class WorkerProfileController: ControllerBase
{
    private readonly IWorkerProfileService _workerProfileService;

    public WorkerProfileController(IWorkerProfileService workerProfileService)
    {
        _workerProfileService = workerProfileService;
    }

    /// <summary>
    /// Получить все профили
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<WorkerProfileDto>>> GetAll()
    {
        var profiles =  await _workerProfileService.GetAllAsync();
        
        return Ok(profiles);
    }

    /// <summary>
    /// Получить свой профиль (Worker)
    /// </summary>
    [HttpGet("my")]
    [Authorize(Roles = "Worker")]
    public async Task<ActionResult<WorkerProfileDto>> GetMyProfile()
    {
        var profile = await _workerProfileService.GetMyProfileAsync();
        
        return Ok(profile);
    }

    /// <summary>
    /// Получить профиль по ID
    /// </summary>
    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<WorkerProfileDto>> GetById(int id)
    {
        var profile = await _workerProfileService.GetByWorkerIdAsync(id);
        
        return Ok(profile);
    }

    /// <summary>
    /// Создать профиль (только Worker, один раз)
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Worker")]
    public async Task<ActionResult<WorkerProfileDto>> Create(CreateWorkerProfileDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var profile = await _workerProfileService.CreateProfileAsync(dto);
        
        return CreatedAtAction(nameof(GetById), new { id = profile.Id }, profile);
    }

    /// <summary>
    /// Обновить свой профиль
    /// </summary>
    [HttpPut]
    [Authorize(Roles = "Worker")]
    public async Task<ActionResult<WorkerProfileDto>> Update(UpdateWorkerProfileDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var profile = await _workerProfileService.UpdateProfileAsync(dto);
        
        return Ok(profile);
    }
}