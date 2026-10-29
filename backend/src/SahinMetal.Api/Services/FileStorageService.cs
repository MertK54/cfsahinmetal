using SahinMetal.Api.DTOs.Upload;
using SahinMetal.Api.Interfaces;

namespace SahinMetal.Api.Services;

public class FileStorageService : IFileStorageService
{
    private readonly IWebHostEnvironment _env;
    private readonly string[] _allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
    private const long MaxFileSizeInBytes = 5 * 1024 * 1024; // 5 MB

    public FileStorageService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<(bool Success, string? ErrorMessage, UploadResponseDto? Result)> UploadImageAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return (false, "Dosya yüklenmedi veya dosya boş.", null);
        }

        if (file.Length > MaxFileSizeInBytes)
        {
            return (false, "Maksimum dosya boyutu 5 MB'tır.", null);
        }

        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!_allowedExtensions.Contains(ext))
        {
            return (false, "Yalnızca resim dosyaları (.jpg, .jpeg, .png, .gif, .webp) yüklenebilir.", null);
        }

        var webRoot = _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot");
        var uploadDir = Path.Combine(webRoot, "uploads");

        if (!Directory.Exists(uploadDir))
        {
            Directory.CreateDirectory(uploadDir);
        }

        var filename = $"{Guid.NewGuid()}{ext}";
        var filePath = Path.Combine(uploadDir, filename);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var fileUrl = $"/uploads/{filename}";
        return (true, null, new UploadResponseDto(fileUrl));
    }
}
