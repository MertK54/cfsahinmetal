namespace SahinMetal.Api.Entities;

public class NewsArticle : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Author { get; set; } = "Admin";
    public DateTime PublishedAt { get; set; } = DateTime.UtcNow;
    public string? ImageUrl { get; set; }
}
