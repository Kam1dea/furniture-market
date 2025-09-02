using Furniture.Application.Dtos.Product;

namespace Furniture.Application.Interfaces.Services;

public interface IProductService
{
    Task<ProductDto> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken ct = default);
    Task<IEnumerable<ProductDto>> GetByWorkerProfileIdAsync(CancellationToken ct = default);
    Task<IEnumerable<ProductDto>> GetMyProductsAsync(CancellationToken ct = default);
    Task<ProductDto> CreateProductAsync(CreateProductDto dto, CancellationToken ct = default);
    Task<ProductDto> UpdateProductAsync(int id, UpdateProductDto dto, CancellationToken ct = default);
    Task DeleteProductAsync(int id, CancellationToken ct = default);
}