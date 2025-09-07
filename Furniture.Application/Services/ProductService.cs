using AutoMapper;
using Furniture.Application.Dtos.Product;
using Furniture.Application.Exceptions;
using Furniture.Domain.Entities;
using Furniture.Application.Interfaces.Repositories;
using Furniture.Application.Interfaces.Services;

namespace Furniture.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWorkerProfileRepository _workerProfileRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IImageService _imageService;
    private readonly IProductImageRepository _productImageRepository;

    public ProductService(IProductRepository productRepository, IMapper mapper, IUnitOfWork unitOfWork,
        IWorkerProfileRepository workerProfileRepository, ICurrentUserService currentUserService,
        IImageService imageService, IProductImageRepository productImageRepository)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _workerProfileRepository = workerProfileRepository;
        _currentUserService = currentUserService;
        _imageService = imageService;
        _productImageRepository = productImageRepository;
    }

    public async Task<ProductDto> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var product = await _productRepository.GetByIdAsync(id, ct);
        if (product == null) return null;

        var dto = _mapper.Map<ProductDto>(product);

        dto.ImageUrls = product.ProductImages.Select(i => i.Url!).ToList();
        dto.MainImageUrl = product.ProductImages.FirstOrDefault(i => i.IsMain)?.Url;

        return dto;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken ct = default)
    {
        var products = await _productRepository.GetAllAsync(ct);

        var dtos = _mapper.Map<IEnumerable<ProductDto>>(products);

        foreach (var dto in dtos)
        {
            var product = products.FirstOrDefault(p => p.Id == dto.Id);
            dto.ImageUrls = product.ProductImages.Select(i => i.Url!).ToList();
            dto.MainImageUrl = product.ProductImages.FirstOrDefault(i => i.IsMain)?.Url;
        }

        return dtos;
    }

    public async Task<IEnumerable<ProductDto>> GetByWorkerProfileIdAsync(int workerProfileId,
        CancellationToken ct = default)
    {
        var products = await _productRepository.GetByWorkerProfileIdAsync(workerProfileId, ct);

        var dtos = _mapper.Map<IEnumerable<ProductDto>>(products);

        foreach (var dto in dtos)
        {
            var prodcut = products.FirstOrDefault(p => p.Id == dto.Id);
            dto.ImageUrls = prodcut.ProductImages.Select(i => i.Url!).ToList();
            dto.MainImageUrl = prodcut.ProductImages.FirstOrDefault(i => i.IsMain)?.Url;
        }

        return dtos;
    }

    public async Task<IEnumerable<ProductDto>> GetMyProductsAsync(CancellationToken ct = default)
    {
        var workerId = _currentUserService.UserId
                       ?? throw new UnauthorizedAccessException("User is not authenticated");

        var workerProfile = await _workerProfileRepository.GetByWorkerIdAsync(workerId, ct)
                            ?? throw new UnauthorizedAccessException(
                                "You must have a worker profile to create products");

        var products = await _productRepository.GetByWorkerProfileIdAsync(workerProfile.Id, ct);

        var dtos = _mapper.Map<IEnumerable<ProductDto>>(products);

        foreach (var dto in dtos)
        {
            var prodcut = products.FirstOrDefault(p => p.Id == dto.Id);
            dto.ImageUrls = prodcut.ProductImages.Select(i => i.Url!).ToList();
            dto.MainImageUrl = prodcut.ProductImages.FirstOrDefault(i => i.IsMain)?.Url;
        }

        return dtos;
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductWithImageDto dto, CancellationToken ct = default)
    {
        var workerId = _currentUserService.UserId
                       ?? throw new UnauthorizedAccessException("User is not authenticated");

        var workerProfile = await _workerProfileRepository.GetByWorkerIdAsync(workerId, ct)
                            ?? throw new UnauthorizedAccessException(
                                "You must have a worker profile to create products");

        var product = _mapper.Map<Product>(dto);
        product.WorkerProfileId = workerProfile.Id;
        product.CreatedOn = DateTime.UtcNow;
        product.ModifiedOn = DateTime.UtcNow;
        product.WorkerName = _currentUserService.Name!;

        await _productRepository.AddAsync(product, ct);
        await _unitOfWork.SaveAsync(ct);

        if (dto.Images.Any())
        {
            var imageUrls = await _imageService.SaveProductImageAsync(dto.Images, product.Id, ct);

            var images = imageUrls.Select((url, index) => new ProductImage
            {
                Url = url,
                IsMain = index == 0,
                ProductId = product.Id,
            }).ToList();

            await _productImageRepository.AddRangeAsync(images, ct);
            await _unitOfWork.SaveAsync(ct);
        }

        return _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> UpdateProductAsync(int id, UpdateProductDto dto, CancellationToken ct = default)
    {
        var workerId = _currentUserService.UserId
                       ?? throw new UnauthorizedAccessException("User is not authenticated");

        var product = await _productRepository.GetByIdAsync(id, ct)
                      ?? throw new NotFoundException("Product not found");

        if (product.WorkerProfile.WorkerId != workerId)
            throw new UnauthorizedAccessException("You can only edit your own products");

        product.ModifiedOn = DateTime.UtcNow;
        _mapper.Map(dto, product);
        _productRepository.UpdateAsync(product, ct);
        await _unitOfWork.SaveAsync(ct);

        return _mapper.Map<ProductDto>(product);
    }

    public async Task DeleteProductAsync(int id, CancellationToken ct = default)
    {
        var workerId = _currentUserService.UserId
                       ?? throw new UnauthorizedAccessException("User is not authenticated");

        var product = await _productRepository.GetByIdAsync(id, ct)
                      ?? throw new NotFoundException("Product not found");

        if (product.WorkerProfile.WorkerId != workerId)
            throw new UnauthorizedAccessException("You can only delete your own products");

        _productRepository.DeleteAsync(product, ct);
        await _unitOfWork.SaveAsync(ct);
    }
}