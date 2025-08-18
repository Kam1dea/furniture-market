using Furniture.Application.Services;
using Furniture.Domain.Interfaces;
using Furniture.Infrastructure.Persistence;
using Furniture.Infrastructure.Repositories;
using Furniture.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Furniture.Infrastructure;

public static class DependencyInjection
{
    
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        
        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
        
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IWorkerProfileRepository, WorkerProfileRepository>();
        services.AddScoped<ITokenService, TokenService>();
        return services;
    }
}