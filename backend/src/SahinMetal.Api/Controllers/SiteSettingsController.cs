using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SahinMetal.Api.Interfaces;

namespace SahinMetal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SiteSettingsController : ControllerBase
{
    private readonly ISiteSettingService _siteSettingService;

    public SiteSettingsController(ISiteSettingService siteSettingService)
    {
        _siteSettingService = siteSettingService;
    }

    // GET: api/sitesettings
    [HttpGet]
    public async Task<ActionResult<Dictionary<string, string>>> GetSettings()
    {
        var settings = await _siteSettingService.GetSettingsAsync();
        return Ok(settings);
    }

    // PUT: api/sitesettings
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateSettings([FromBody] Dictionary<string, string> newSettings)
    {
        await _siteSettingService.UpdateSettingsAsync(newSettings);
        return NoContent();
    }
}
