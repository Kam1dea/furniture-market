using System.Security.Claims;
using AutoMapper;
using Furniture.Application.Dtos;
using Furniture.Application.Dtos.Product;
using Furniture.Application.Exceptions;
using Furniture.Domain.Entities;
using Furniture.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Furniture.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private UserManager<User> _userManager;

    public ProductController(IProductRepository productRepository, IMapper mapper, UserManager<User> userManager)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<Product>>(products));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            throw new NotFoundException($"Product with ID {id} not found.");
        }

        return Ok(_mapper.Map<Product>(product));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateProductDto product)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId);
        
        var productEntity = _mapper.Map<Product>(product);
        product.WorkerProfileId = user.WorkerProfile.Id;
        await _productRepository.CreateAsync(productEntity);
        return Ok(_mapper.Map<ProductDto>(productEntity));
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateProductDto product)
    {
        var productEntity = await _productRepository.GetByIdAsync(id);
        if (productEntity == null)
        {
            throw new NotFoundException($"Product with ID {id} not found.");
        }
        _mapper.Map(product, productEntity);
        await _productRepository.UpdateAsync(id, productEntity);
        
        return Ok(_mapper.Map<ProductDto>(productEntity));
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        await _productRepository.DeleteAsync(id);

        return NoContent();
    }
}