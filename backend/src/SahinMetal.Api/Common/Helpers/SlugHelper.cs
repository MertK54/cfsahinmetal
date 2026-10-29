using System.Text.RegularExpressions;

namespace SahinMetal.Api.Common.Helpers;

public static class SlugHelper
{
    public static string GenerateSlug(string phrase)
    {
        if (string.IsNullOrWhiteSpace(phrase)) return string.Empty;

        string str = phrase.ToLowerInvariant();

        // Replace Turkish specific characters
        str = str.Replace("ğ", "g")
                 .Replace("ü", "u")
                 .Replace("ş", "s")
                 .Replace("ı", "i")
                 .Replace("ö", "o")
                 .Replace("ç", "c");

        // Remove invalid characters
        str = Regex.Replace(str, @"[^a-z0-9\s-]", "");

        // Convert multiple spaces into a single space
        str = Regex.Replace(str, @"\s+", " ").Trim();

        // Replace spaces with hyphens
        str = Regex.Replace(str, @"\s", "-");

        return str;
    }
}
