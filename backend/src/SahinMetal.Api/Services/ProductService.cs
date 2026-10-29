using Microsoft.EntityFrameworkCore;
using SahinMetal.Api.Common.Helpers;
using SahinMetal.Api.Data;
using SahinMetal.Api.DTOs.Product;
using SahinMetal.Api.Entities;
using SahinMetal.Api.Interfaces;

namespace SahinMetal.Api.Services;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync(bool includeInactive = false)
    {
        var query = _context.Products.AsQueryable();
        if (!includeInactive)
        {
            query = query.Where(p => p.IsActive);
        }
        return await query.ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product?> GetProductBySlugAsync(string slug)
    {
        return await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Slug == slug);
    }

    public async Task<Product> CreateProductAsync(ProductDto dto)
    {
        var safeName = SecurityHelper.SanitizeText(dto.Name, 200);
        var safeDescription = SecurityHelper.SanitizeText(dto.Description, 4000);
        var safeSpecifications = SecurityHelper.SanitizeText(dto.Specifications, 500);
        var safeMaterialType = SecurityHelper.SanitizeText(dto.MaterialType, 150);
        var safeDimensions = SecurityHelper.SanitizeText(dto.Dimensions, 100);

        var slug = SlugHelper.GenerateSlug(safeName);
        if (await _context.Products.AnyAsync(p => p.Slug == slug))
        {
            slug = $"{slug}-{Guid.NewGuid().ToString().Substring(0, 5)}";
        }

        var product = new Product
        {
            CategoryId = dto.CategoryId,
            Name = safeName,
            Slug = slug,
            Description = safeDescription,
            Specifications = safeSpecifications,
            MaterialType = safeMaterialType,
            Weight = dto.Weight,
            Dimensions = safeDimensions,
            ImageUrl = dto.ImageUrl,
            IsActive = true
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<bool> UpdateProductAsync(int id, ProductDto dto)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return false;
        }

        var safeName = SecurityHelper.SanitizeText(dto.Name, 200);
        var safeDescription = SecurityHelper.SanitizeText(dto.Description, 4000);
        var safeSpecifications = SecurityHelper.SanitizeText(dto.Specifications, 500);
        var safeMaterialType = SecurityHelper.SanitizeText(dto.MaterialType, 150);
        var safeDimensions = SecurityHelper.SanitizeText(dto.Dimensions, 100);

        var slug = SlugHelper.GenerateSlug(safeName);
        if (product.Name != safeName && await _context.Products.AnyAsync(p => p.Slug == slug && p.Id != id))
        {
            slug = $"{slug}-{Guid.NewGuid().ToString().Substring(0, 5)}";
        }

        product.CategoryId = dto.CategoryId;
        product.Name = safeName;
        product.Slug = slug;
        product.Description = safeDescription;
        product.Specifications = safeSpecifications;
        product.MaterialType = safeMaterialType;
        product.Weight = dto.Weight;
        product.Dimensions = safeDimensions;
        if (!string.IsNullOrEmpty(dto.ImageUrl))
        {
            product.ImageUrl = dto.ImageUrl;
        }
        product.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return false;
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
}
