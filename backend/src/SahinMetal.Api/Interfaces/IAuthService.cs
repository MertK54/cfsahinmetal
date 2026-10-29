using SahinMetal.Api.DTOs.Auth;

namespace SahinMetal.Api.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);
}
