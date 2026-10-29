using Microsoft.AspNetCore.Mvc;
using SahinMetal.Api.DTOs.Auth;
using SahinMetal.Api.Interfaces;

namespace SahinMetal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var response = await _authService.LoginAsync(request);
        if (response == null)
        {
            return Unauthorized("Geçersiz kullanıcı adı veya şifre.");
        }

        return Ok(response);
    }
}
