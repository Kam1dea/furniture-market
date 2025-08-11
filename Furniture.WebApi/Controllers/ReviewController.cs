using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Furniture.Application.Dtos;
using Furniture.Application.Dtos.Review;
using Furniture.Application.Exceptions;
using Furniture.Domain.Entities;
using Furniture.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Furniture.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewController: ControllerBase
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public ReviewController(IReviewRepository reviewRepository, IMapper mapper, UserManager<User> userManager)
    {
        _reviewRepository = reviewRepository;
        _mapper = mapper;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var reviews = await _reviewRepository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<Review>>(reviews));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var review = await _reviewRepository.GetByIdAsync(id);

        if (review == null)
        {
            throw new NotFoundException($"Product with ID {id} not found.");
        }
        
        return Ok(_mapper.Map<Review>(review));
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] CreateReviewDto reviewDto)
    {
        // if (!review.ProductId.HasValue && !review.WorkerProfileId.HasValue)
        //     throw new ValidationException("Review must have a productId or WorkerProfileId");
        // if (review.ProductId.HasValue && review.WorkerProfileId.HasValue)
        //     throw new ValidationException("Review cannot have both ProductId and WorkerProfileId");
        //var username = User.Identity.Name;
        //var user = await _userManager.FindByNameAsync(username);
        
        var reviewEntity = _mapper.Map<Review>(reviewDto);
        //reviewEntity.UserId = user.Id;
        await _reviewRepository.CreateAsync(reviewEntity);
        return Ok(_mapper.Map<ReviewDto>(reviewEntity));
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateReviewDto review)
    {
        var reviewEntity = await _reviewRepository.GetByIdAsync(id);
        if (reviewEntity == null)
        {
            throw new NotFoundException($"Product with ID {id} not found.");
        }
        _mapper.Map(review, reviewEntity);
        await _reviewRepository.UpdateAsync(id, reviewEntity);
        return Ok(_mapper.Map<Review>(reviewEntity));
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        await _reviewRepository.DeleteAsync(id);
        
        return NoContent();
    }
}