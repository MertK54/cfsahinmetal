using Microsoft.EntityFrameworkCore;
using SahinMetal.Api.Data;
using SahinMetal.Api.Entities;
using SahinMetal.Api.Interfaces;

namespace SahinMetal.Api.Services;

public class SiteSettingService : ISiteSettingService
{
    private readonly ApplicationDbContext _context;

    public SiteSettingService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Dictionary<string, string>> GetSettingsAsync()
    {
        var settings = await _context.SiteSettings.ToListAsync();
        return settings.ToDictionary(s => s.Key, s => s.Value);
    }

    public async Task UpdateSettingsAsync(Dictionary<string, string> newSettings)
    {
        foreach (var (key, value) in newSettings)
        {
            var setting = await _context.SiteSettings.FindAsync(key);
            if (setting != null)
            {
                setting.Value = value;
            }
            else
            {
                _context.SiteSettings.Add(new SiteSetting { Key = key, Value = value });
            }
        }

        await _context.SaveChangesAsync();
    }
}
