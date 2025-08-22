using Xunit;
using FluentAssertions;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Furniture.Domain.Entities;
using Furniture.Application.Dtos;
using Furniture.Application.Dtos.Account;
using Furniture.Application.Dtos.Product;
using Furniture.Infrastructure.Persistence;
using Furniture.Order.Tests.Shared;

namespace Furniture.Order.Tests.Integration;

public class ProductControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;

    public ProductControllerTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetProducts_ReturnsOk()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Products.Add(new Product
        {
            Id = 1,
            Name = "Test Table",
            Price = 500,
            Category = "Table",
            WorkerProfileId = 1
            
        });
        await context.SaveChangesAsync();

        // Act
        var response = await _client.GetAsync("/api/Product");

        // Assert
        response.EnsureSuccessStatusCode();
        var products = await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
        products.Should().HaveCount(1);
        products.First().Name.Should().Be("Test Table");
    }

    [Fact]
    public async Task CreateProduct_WithoutAuth_ReturnsUnauthorized()
    {
        // Arrange
        var command = new CreateProductDto
        {
            Name = "New Chair",
            Price = 300,
            Category = "Chair"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/Product", command);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task CreateProduct_AsWorker_ReturnsOk()
    {
        // Arrange
        var loginDto = new LoginDto
        {
            Email = "worker@worker.com",
            Password = "Worker-12345"
        };

        // Получаем JWT
        var authResponse = await _client.PostAsJsonAsync("/api/Auth/login", loginDto);
        var authContent = await authResponse.Content.ReadFromJsonAsync<AuthResponseDto>();
        var token = authContent?.Token;

        // Настраиваем клиент с токеном
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var command = new CreateProductDto
        {
            Name = "Luxury Table",
            Price = 2000,
            Category = "Table",
            Width = 120,
            Height = 80,
            Depth = 60
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/products", command);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var createdProduct = await response.Content.ReadFromJsonAsync<ProductDto>();
        createdProduct!.Name.Should().Be("Luxury Table");
        createdProduct.Price.Should().Be(2000);
    }
}