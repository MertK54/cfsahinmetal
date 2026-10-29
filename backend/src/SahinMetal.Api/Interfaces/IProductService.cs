using SahinMetal.Api.DTOs.Product;
using SahinMetal.Api.Entities;

namespace SahinMetal.Api.Interfaces;

public interface IProductService
{
    Task<IEnumerable<Product>> GetProductsAsync(bool includeInactive = false);
    Task<Product?> GetProductByIdAsync(int id);
    Task<Product?> GetProductBySlugAsync(string slug);
    Task<Product> CreateProductAsync(ProductDto dto);
    Task<bool> UpdateProductAsync(int id, ProductDto dto);
    Task<bool> DeleteProductAsync(int id);
}
