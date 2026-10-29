using System.ComponentModel.DataAnnotations;

namespace SahinMetal.Api.DTOs.Product;

public record ProductDto(
    [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir kategori seçilmelidir.")]
    int CategoryId,

    [Required(ErrorMessage = "Ürün veya iş başlığı zorunludur.")]
    [StringLength(200, ErrorMessage = "Başlık en fazla 200 karakter olabilir.")]
    string Name,

    [Required(ErrorMessage = "Açıklama alanı zorunludur.")]
    [StringLength(4000, ErrorMessage = "Açıklama en fazla 4000 karakter olabilir.")]
    string Description,

    [StringLength(500, ErrorMessage = "Teknik özellikler en fazla 500 karakter olabilir.")]
    string? Specifications,

    [StringLength(150, ErrorMessage = "Malzeme tipi en fazla 150 karakter olabilir.")]
    string? MaterialType,

    decimal? Weight,

    [StringLength(100, ErrorMessage = "Ölçüler en fazla 100 karakter olabilir.")]
    string? Dimensions,

    [StringLength(500, ErrorMessage = "Görsel URL en fazla 500 karakter olabilir.")]
    string? ImageUrl,

    bool IsActive = true
);
