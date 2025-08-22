using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Furniture.Domain.Entities;
using Furniture.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Furniture.Order.Tests.Shared;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Удаляем реальный DbContext
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
            if (descriptor != null)
                services.Remove(descriptor);

            // Добавляем InMemory DB
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("TestDb"));

            // Удаляем реальный Identity
            services.RemoveAll<IdentityUser>();

           

            // Выполняем миграции (или создаём данные)
            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Создаём тестового пользователя (Worker)
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var user = new User
            {
                UserName = "worker@test.com",
                Email = "worker@test.com",
                FirstName = "Test",
                LastName = "Worker"
            };
            userManager.CreateAsync(user, "Password123!").Wait();
            //userManager.AddToRoleAsync(user, "Worker").Wait();
        });
    }
}