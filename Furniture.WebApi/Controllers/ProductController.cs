using AutoMapper;
using Furniture.Application.Dtos;
using Furniture.Application.Dtos.Product;
using Furniture.Domain.Entities;
using Furniture.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Furniture.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductController(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var products = await _productRepository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<Product>>(products));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<Product>(product));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateProductDto product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var productEntity = _mapper.Map<Product>(product);
        await _productRepository.CreateAsync(productEntity);
        return Ok(_mapper.Map<ProductDto>(productEntity));
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateProductDto product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var productEntity = await _productRepository.GetByIdAsync(id);
        if (productEntity == null)
        {
            return NotFound();
        }
        _mapper.Map(product, productEntity);
        await _productRepository.UpdateAsync(id, productEntity);
        
        return Ok(_mapper.Map<ProductDto>(productEntity));
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _productRepository.DeleteAsync(id);

        return NoContent();
    }
}