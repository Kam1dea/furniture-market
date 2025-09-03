using Microsoft.AspNetCore.Http;

namespace Furniture.Application.Interfaces.Services;

public interface IImageService
{
    Task<List<string>> SaveProductImageAsync(List<IFormFile> files, int productId, CancellationToken ct = default);
    Task DeleteProductImageAsync(int productId, List<string> imageUrls, CancellationToken ct = default);
    string GetImageUrl(string fileName);
}