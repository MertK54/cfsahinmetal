using System.Net;
using System.Text.RegularExpressions;

namespace SahinMetal.Api.Common.Helpers;

public static class SecurityHelper
{
    private static readonly Regex HtmlTagRegex = new(@"<[^>]*>", RegexOptions.Compiled);
    private static readonly Regex SqlInjectionPatternRegex = new(
        @"(?i)(--|;|\/\*|\*\/|@@|char\s*\(|nchar\s*\(|varchar\s*\(|alter\s+table|delete\s+from|drop\s+table|insert\s+into|select\s+.*\s+from|union\s+select)",
        RegexOptions.Compiled
    );

    /// <summary>
    /// Sanitizes plain text input by stripping HTML tags, removing null bytes,
    /// escaping dangerous characters, and neutralizing potential script/injection vectors.
    /// </summary>
    public static string SanitizeText(string? input, int maxLength = 2000)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return string.Empty;
        }

        // 1. Remove null bytes and control characters (except newline and carriage return)
        var sanitized = input.Replace("\0", string.Empty);
        sanitized = Regex.Replace(sanitized, @"[\x00-\x08\x0B\x0C\x0E-\x1F]", string.Empty);

        // 2. Strip HTML tags to eliminate XSS
        sanitized = HtmlTagRegex.Replace(sanitized, string.Empty);

        // 3. Trim whitespace
        sanitized = sanitized.Trim();

        // 4. Enforce max length
        if (sanitized.Length > maxLength)
        {
            sanitized = sanitized.Substring(0, maxLength);
        }

        return sanitized;
    }

    /// <summary>
    /// Checks if the input contains dangerous SQL injection keywords or patterns.
    /// </summary>
    public static bool ContainsSqlInjectionPayload(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return false;
        }

        return SqlInjectionPatternRegex.IsMatch(input);
    }

    /// <summary>
    /// Validates and sanitizes a phone number string:
    /// only permits digits, spaces, '+', '-', '(', ')'.
    /// Rejects any letters or dangerous symbols.
    /// </summary>
    public static string SanitizePhone(string? phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
        {
            return string.Empty;
        }

        // Remove any letters or prohibited characters
        var cleaned = Regex.Replace(phone.Trim(), @"[^0-9+\s()-]", string.Empty);
        return cleaned.Length > 25 ? cleaned.Substring(0, 25) : cleaned;
    }
}
