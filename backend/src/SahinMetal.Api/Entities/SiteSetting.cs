using System.ComponentModel.DataAnnotations;

namespace SahinMetal.Api.Entities;

public class SiteSetting
{
    [Key]
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}
