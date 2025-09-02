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
    /// Получить все товары
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
    {
        var products = await _productService.GetAllAsync();
        return Ok(products);
    }

    /// <summary>
    /// Получить товар по ID
    /// </summary>
    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<ProductDto>> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        return Ok(product);
    }
    
    /// <summary>
    /// Получить товары текущего работника
    /// </summary>
    [HttpGet("my")]
    [Authorize(Roles = "Worker")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetMyProducts()
    {
        var products = await _productService.GetMyProductsAsync();
        return Ok(products);
    }

    /// <summary>
    /// Создать новый товар (только Worker)
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Worker")]
    public async Task<ActionResult<ProductDto>> Create(CreateProductDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var product = await _productService.CreateProductAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    /// <summary>
    /// Обновить товар (только владелец)
    /// </summary>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Worker")]
    public async Task<ActionResult<ProductDto>> Update(int id, UpdateProductDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        await _productService.UpdateProductAsync(id, dto);
        return NoContent();
    }

    /// <summary>
    /// Удалить товар (только владелец)
    /// </summary>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Worker")]
    public async Task<ActionResult> Delete(int id)
    {
        await _productService.DeleteProductAsync(id);
        return NoContent();
    }
}