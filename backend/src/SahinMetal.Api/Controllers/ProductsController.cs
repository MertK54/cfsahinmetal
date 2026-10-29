using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SahinMetal.Api.DTOs.Product;
using SahinMetal.Api.Entities;
using SahinMetal.Api.Interfaces;

namespace SahinMetal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    // GET: api/products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] bool includeInactive = false)
    {
        var products = await _productService.GetProductsAsync(includeInactive);
        return Ok(products);
    }

    // GET: api/products/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound("Ürün bulunamadı.");
        }
        return Ok(product);
    }

    // GET: api/products/slug/dokum-tasima-arabalari
    [HttpGet("slug/{slug}")]
    public async Task<ActionResult<Product>> GetProductBySlug(string slug)
    {
        var product = await _productService.GetProductBySlugAsync(slug);
        if (product == null)
        {
            return NotFound("Ürün bulunamadı.");
        }
        return Ok(product);
    }

    // POST: api/products
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] ProductDto dto)
    {
        var product = await _productService.CreateProductAsync(dto);
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    // PUT: api/products/5
    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto dto)
    {
        var success = await _productService.UpdateProductAsync(id, dto);
        if (!success)
        {
            return NotFound("Ürün bulunamadı.");
        }
        return NoContent();
    }

    // DELETE: api/products/5
    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var success = await _productService.DeleteProductAsync(id);
        if (!success)
        {
            return NotFound("Ürün bulunamadı.");
        }
        return NoContent();
    }
}
