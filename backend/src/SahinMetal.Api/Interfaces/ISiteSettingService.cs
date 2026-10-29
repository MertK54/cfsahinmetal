namespace SahinMetal.Api.Interfaces;

public interface ISiteSettingService
{
    Task<Dictionary<string, string>> GetSettingsAsync();
    Task UpdateSettingsAsync(Dictionary<string, string> newSettings);
}
