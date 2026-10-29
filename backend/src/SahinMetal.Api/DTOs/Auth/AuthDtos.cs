using System.ComponentModel.DataAnnotations;

namespace SahinMetal.Api.DTOs.Auth;

public record LoginRequestDto(
    [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
    [StringLength(50, ErrorMessage = "Kullanıcı adı en fazla 50 karakter olabilir.")]
    string Username,

    [Required(ErrorMessage = "Şifre zorunludur.")]
    [StringLength(100, ErrorMessage = "Şifre en fazla 100 karakter olabilir.")]
    string Password
);

public record LoginResponseDto(string Token, string Username, string Role);
