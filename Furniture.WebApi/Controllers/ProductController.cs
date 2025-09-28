using Furniture.Application.Dtos.Product;
using Furniture.Application.Exceptions;
using Furniture.Application.Interfaces.Repositories;
using Furniture.Application.Interfaces.Services;
using Furniture.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Furniture.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService  _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>
    /// Получить все товары.
    /// </summary>
    /// <returns>
    /// Список всех товаров.
    /// </returns>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
    {
        var products = await _productService.GetAllAsync();
        return Ok(products);
    }

    /// <summary>
    /// Получить товар по ID.
    /// </summary>
    /// <param name="id">
    /// Id продукта.
    /// </param>
    /// <returns>
    /// Продукт по указанному Id.
    /// </returns>
    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<ProductDto>> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        return Ok(product);
    }
    
    /// <summary>
    /// Получить товары текущего работника.
    /// </summary>
    /// <returns>
    /// Товары текущего работника.
    /// </returns>
    [HttpGet("my")]
    [Authorize(Roles = "Worker")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetMyProducts()
    {
        var products = await _productService.GetMyProductsAsync();
        return Ok(products);
    }

    /// <summary>
    /// Получить товары по ID профиля работника.
    /// </summary>
    /// <param name="id">Id профиля работника.</param>
    /// <returns>Товары работника по переданному Id.</returns>
    [HttpGet("worker-profile/{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetByWorkerProfileId(int id)
    {
        var products = await _productService.GetByWorkerProfileIdAsync(id);
        return Ok(products);
    }
    
    /// <summary>
    /// Создать новый товар (только Worker).
    /// </summary>
    [HttpPost("create-with-image")]
    [Authorize(Roles = "Worker")]
    public async Task<ActionResult<ProductDto>> Create([FromForm] CreateProductWithImageDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var product = await _productService.CreateProductAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    /// <summary>
    /// Обновить товар (только владелец).
    /// </summary>
    [HttpPut("update-with-images/{id:int}")]
    [Authorize(Roles = "Worker")]
    public async Task<ActionResult<ProductDto>> Update(int id, [FromForm] UpdateProductWithImageDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        await _productService.UpdateProductAsync(id, dto);
        return NoContent();
    }

    /// <summary>
    /// Удалить товар (только владелец).
    /// </summary>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Worker")]
    public async Task<ActionResult> Delete(int id)
    {
        await _productService.DeleteProductAsync(id);
        return NoContent();
    }
}