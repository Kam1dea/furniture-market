using Furniture.Application.Interfaces.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Furniture.Application.Services;

public class ImageService : IImageService
{
    private readonly IWebHostEnvironment _env;
    private readonly string _uploadsPath;
    private readonly string _baseUrl;

    public ImageService(IWebHostEnvironment env, IConfiguration configuration)
    {
        _env = env;
        _uploadsPath = Path.Combine(_env.WebRootPath, "uploads", "products");
        _baseUrl = configuration["App:BaseUrl"] ?? "http://localhost:5096";
    }

    public async Task<List<string>> SaveProductImageAsync(List<IFormFile> files, int productId,
        CancellationToken ct = default)
    {
        if (!Directory.Exists(_uploadsPath))
            Directory.CreateDirectory(_uploadsPath);

        var urls = new List<string>();
        var allowedTypes = new [] {"image/jpeg", "image/jpg", "image/png", "image/gif"};
        var maxFileSize = 5 * 1024 * 1024;

        foreach (var file in files)
        {
            if (file.Length == 0) continue;
            if(!allowedTypes.Contains(file.ContentType)) 
                throw new InvalidDataException($"File type {file.ContentType} is not allowed.");
            if (file.Length > maxFileSize)
                throw new InvalidOperationException($"File size limit {maxFileSize} exceeded.");

            var fileName = $"{productId}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(_uploadsPath, fileName);
            var url = $"{_baseUrl}/uploads/products/{fileName}";

            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            
            urls.Add(url);
        }
        return urls;
    }

    public async Task DeleteProductImageAsync(int productId, List<string> imageUrls, CancellationToken ct = default)
    {
        foreach (var url in imageUrls)
        {
            var fileName = new Uri(url).Segments.Last();
            var filePath = Path.Combine(_uploadsPath, fileName);

            if (File.Exists(filePath))
                await Task.Run(() => File.Delete(filePath), ct);
        }
    }

    public string GetImageUrl(string fileName)
    {
        return $"{_baseUrl}/uploads/products/{fileName}";
    }
}