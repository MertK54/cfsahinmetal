using System.ComponentModel.DataAnnotations;

namespace SahinMetal.Api.DTOs.Contact;

public record SubmissionDto(
    [Required(ErrorMessage = "Ad Soyad alanı zorunludur.")]
    [StringLength(100, ErrorMessage = "Ad Soyad en fazla 100 karakter olabilir.")]
    string Name,

    [Required(ErrorMessage = "İletişim numarası / e-posta zorunludur.")]
    [StringLength(100, ErrorMessage = "İletişim bilgisi en fazla 100 karakter olabilir.")]
    string Email,

    [Required(ErrorMessage = "Konu alanı zorunludur.")]
    [StringLength(150, ErrorMessage = "Konu en fazla 150 karakter olabilir.")]
    string Subject,

    [Required(ErrorMessage = "Mesaj alanı zorunludur.")]
    [StringLength(3000, ErrorMessage = "Mesaj en fazla 3000 karakter olabilir.")]
    string Message
);
