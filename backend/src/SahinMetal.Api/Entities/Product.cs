namespace SahinMetal.Api.Entities;

public class Product : BaseEntity
{
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    // Stores custom key-value specifications for metal products as JSON
    public string? Specifications { get; set; }
    
    public string? MaterialType { get; set; }
    public decimal? Weight { get; set; }
    public string? Dimensions { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; } = true;
}
