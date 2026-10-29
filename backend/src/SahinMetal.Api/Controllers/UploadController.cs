using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SahinMetal.Api.Interfaces;

namespace SahinMetal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UploadController : ControllerBase
{
    private readonly IFileStorageService _fileStorageService;

    public UploadController(IFileStorageService fileStorageService)
    {
        _fileStorageService = fileStorageService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
    {
        var (success, errorMessage, result) = await _fileStorageService.UploadImageAsync(file);
        if (!success)
        {
            return BadRequest(errorMessage);
        }

        return Ok(result);
    }
}
