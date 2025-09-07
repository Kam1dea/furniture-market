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
    private readonly IImageService _imageService;
    private readonly IReviewImageRepository _reviewImageRepository;

    public ReviewService(
        IReviewRepository reviewRepository,
        ICurrentUserService currentUserService, IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService, IReviewImageRepository reviewImageRepository)
    {
        _reviewRepository = reviewRepository;
        _currentUserService = currentUserService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _imageService = imageService;
        _reviewImageRepository = reviewImageRepository;
    }

    public async Task<IEnumerable<ReviewDto>> GetAllAsync(CancellationToken ct = default)
    {
        var reviews = await _reviewRepository.GetAllAsync(ct);
        
        var dtos = _mapper.Map<IEnumerable<ReviewDto>>(reviews);

        foreach (var dto in dtos)
        {
            var review = reviews.FirstOrDefault(r => r.Id == dto.Id);
            dto.ImagesUrls = review.ReviewImages.Select(i => i.Url!).ToList();
            dto.MainImageUrl = review.ReviewImages.FirstOrDefault(i => i.IsMain)?.Url;
        }
        
        return dtos;
    }

    public async Task<ReviewDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var review = await _reviewRepository.GetByIdAsync(id, ct);
        if (review == null) return null;
        
        var dto = _mapper.Map<ReviewDto>(review);
        
        dto.ImagesUrls = review.ReviewImages.Select(i => i.Url!).ToList();
        dto.MainImageUrl = review.ReviewImages.FirstOrDefault(i => i.IsMain)?.Url;
        
        return dto;
    }

    public async Task<IEnumerable<ReviewDto>> GetByProductAsync(int productId, CancellationToken ct = default)
    {
        var reviews = await _reviewRepository.GetByProductIdAsync(productId, ct);
        
        var dtos = _mapper.Map<IEnumerable<ReviewDto>>(reviews);

        foreach (var dto in dtos)
        {
            var review = reviews.FirstOrDefault(r => r.Id == dto.Id);
            dto.ImagesUrls = review.ReviewImages.Select(i => i.Url!).ToList();
            dto.MainImageUrl = review.ReviewImages.FirstOrDefault(i => i.IsMain)?.Url;
        }
        
        return dtos;
    }

    public async Task<IEnumerable<ReviewDto>> GetMyReviewsAsync(CancellationToken ct = default)
    {
        var userId = _currentUserService.UserId
            ?? throw new UnauthorizedAccessException("User not authenticated");
        
        var reviews = await _reviewRepository.GetByUserIdAsync(userId, ct);
        
        var dtos = _mapper.Map<IEnumerable<ReviewDto>>(reviews);

        foreach (var dto in dtos)
        {
            var review = reviews.FirstOrDefault(r => r.Id == dto.Id);
            dto.ImagesUrls = review.ReviewImages.Select(i => i.Url!).ToList();
            dto.MainImageUrl = review.ReviewImages.FirstOrDefault(i => i.IsMain)?.Url;
        }
        
        return dtos;
    }

    public async Task<ReviewDto> CreateReviewAsync(CreateReviewWithImageDto dto, CancellationToken ct = default)
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
        review.ModifiedOn = DateTime.UtcNow;

        await _reviewRepository.AddAsync(review, ct);
        await _unitOfWork.SaveAsync(ct);

        if (dto.Images.Any())
        {
            var imageUrls = await _imageService.SaveReviewImageAsync(dto.Images, review.Id, ct);

            var images = imageUrls.Select((url, index) => new ReviewImage
            {
                Url = url,
                IsMain = index == 0,
                ReviewId = review.Id,
            }).ToList();
            
            await _reviewImageRepository.AddRangeAsync(images, ct);
            await _unitOfWork.SaveAsync(ct);
        }

        return await GetByIdAsync(review.Id, ct);
    }

    public async Task UpdateReviewAsync(int id, UpdateReviewWithImageDto dto, CancellationToken ct = default)
    {
        var userId = _currentUserService.UserId
                     ?? throw new UnauthorizedAccessException("User not authenticated");

        var review = await _reviewRepository.GetByIdAsync(id, ct)
                     ?? throw new NotFoundException($"Review with ID {id} not found.");

        if (review.UserId != userId)
            throw new UnauthorizedAccessException("You can only update your own reviews.");

        review.Tittle = dto.Tittle;
        review.Content = dto.Content;
        review.Rating = dto.Rating;
        review.ModifiedOn = DateTime.UtcNow;

        await _reviewRepository.UpdateAsync(review, ct);
        await _unitOfWork.SaveAsync(ct);
        
        if (dto.NewImages.Any())
        {
            var imageUrls = await _imageService.SaveReviewImageAsync(dto.NewImages, review.Id, ct);

            var newImages = imageUrls.Select(url => new ReviewImage
            {
                Url = url,
                IsMain = false,
                ReviewId = review.Id
            }).ToList();

            _reviewImageRepository.AddRangeAsync(newImages);
            await _unitOfWork.SaveAsync(ct);
        }
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
