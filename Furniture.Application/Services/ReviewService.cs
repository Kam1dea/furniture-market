using AutoMapper;
using Furniture.Application.Dtos.Review;
using Furniture.Application.Exceptions;
using Furniture.Application.Interfaces.Repositories;
using Furniture.Application.Interfaces.Services;
using Furniture.Domain.Entities;

namespace Furniture.Application.Services;

public class ReviewService: IReviewService
{
    private readonly IReviewRepository _reviewRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork  _unitOfWork;
    private readonly IMapper _mapper;

    public ReviewService(
        IReviewRepository reviewRepository,
        ICurrentUserService currentUserService, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _reviewRepository = reviewRepository;
        _currentUserService = currentUserService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ReviewDto>> GetAllAsync(CancellationToken ct = default)
    {
        var reviews = await _reviewRepository.GetAllAsync(ct);
        
        return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
    }

    public async Task<ReviewDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var review = await _reviewRepository.GetByIdAsync(id, ct);
        if (review == null) return null;
        
        return _mapper.Map<ReviewDto>(review);
    }

    public async Task<IEnumerable<ReviewDto>> GetByProductAsync(int productId, CancellationToken ct = default)
    {
        var reviews = await _reviewRepository.GetByProductIdAsync(productId, ct);
        
        return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
    }

    public async Task<IEnumerable<ReviewDto>> GetMyReviewsAsync(CancellationToken ct = default)
    {
        var userId = _currentUserService.UserId
            ?? throw new UnauthorizedAccessException("User not authenticated");
        
        var reviews = await _reviewRepository.GetByUserIdAsync(userId, ct);
        
        return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
    }

    public async Task<int> CreateReviewAsync(CreateReviewDto dto, CancellationToken ct = default)
    {
        var userId = _currentUserService.UserId
            ?? throw new UnauthorizedAccessException("User not authenticated");

        // Проверка на наличие отзыва на продукт
        var alreadyExists = await _reviewRepository.ExistsByUserAndProductAsync(userId, dto.ProductId, ct);
        if (alreadyExists)
            throw new InvalidOperationException("You have already reviewed this product.");
        
        var review = _mapper.Map<Review>(dto);
        review.UserId = userId;
        review.CreatedOn = DateTime.UtcNow;

        await _reviewRepository.AddAsync(review, ct);
        await _unitOfWork.SaveAsync(ct);

        return review.Id;
    }

    public async Task UpdateReviewAsync(int id, UpdateReviewDto dto, CancellationToken ct = default)
    {
        var userId = _currentUserService.UserId
                     ?? throw new UnauthorizedAccessException("User not authenticated");

        var review = await _reviewRepository.GetByIdAsync(id, ct)
                     ?? throw new NotFoundException($"Review with ID {id} not found.");

        // Проверяем: это его отзыв?
        if (review.UserId != userId)
            throw new UnauthorizedAccessException("You can only update your own reviews.");

        // Обновляем поля
        review.Tittle = dto.Tittle;
        review.Content = dto.Content;
        review.Rating = dto.Rating;
        review.ModifiedOn = DateTime.UtcNow;

        _reviewRepository.UpdateAsync(review, ct);
        await _unitOfWork.SaveAsync(ct);
    }

    public async Task DeleteReviewAsync(int id, CancellationToken ct = default)
    {
        var userId = _currentUserService.UserId
                     ?? throw new UnauthorizedAccessException("User not authenticated");

        var review = await _reviewRepository.GetByIdAsync(id, ct)
                     ?? throw new NotFoundException($"Review with ID {id} not found.");

        // Проверяем владение
        if (review.UserId != userId)
            throw new UnauthorizedAccessException("You can only delete your own reviews.");

        await _reviewRepository.DeleteAsync(review, ct);
        await _unitOfWork.SaveAsync(ct);
    }
}
