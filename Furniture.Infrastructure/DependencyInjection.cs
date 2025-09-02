using Furniture.Application.Interfaces;
using Furniture.Application.Interfaces.Repositories;
using Furniture.Application.Interfaces.Services;
using Furniture.Application.Services;
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

        services.AddHttpContextAccessor();
        
        services.AddScoped<ITokenService, TokenService>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();
        
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IReviewService, ReviewService>();
        
        services.AddScoped<IWorkerProfileRepository, WorkerProfileRepository>();
        
        
        return services;
    }
}