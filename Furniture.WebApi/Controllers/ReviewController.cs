using Furniture.Application.Dtos.Review;
using Furniture.Application.Exceptions;
using Furniture.Application.Interfaces.Services;
using Furniture.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Furniture.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewController: ControllerBase
{
    private readonly IReviewService  _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    /// <summary>
    /// Получить все отзывы
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAll()
    {
        var reviews = await _reviewService.GetAllAsync();
        return Ok(reviews);
    }

    /// <summary>
    /// Получить отзыв по ID
    /// </summary>
    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<ReviewDto>> GetById(int id)
    {
        var review = await _reviewService.GetByIdAsync(id);
        if (review == null)
            return NotFound();
        
        return Ok(review);
    }

    /// <summary>
    /// Получить все отзывы для товара
    /// </summary>
    [HttpGet("my")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetMyReviews()
    {
        var reviews = await _reviewService.GetMyReviewsAsync();
        return Ok(reviews);
    }

    /// <summary>
    /// Создать новый отзыв
    /// </summary>
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<int>> Create(CreateReviewDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var reviewId = await _reviewService.CreateReviewAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = reviewId }, reviewId);
    }

    /// <summary>
    /// Обновить свой отзыв
    /// </summary>
    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<ActionResult> Update(int id, UpdateReviewDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        await _reviewService.UpdateReviewAsync(id, dto);
        return NoContent();
    }

    /// <summary>
    /// Удалить свой отзыв
    /// </summary>
    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<ActionResult> Delete(int id)
    {
        await _reviewService.DeleteReviewAsync(id);
        return NoContent();
    }
}