using Microsoft.AspNetCore.Http;
using SahinMetal.Api.DTOs.Upload;

namespace SahinMetal.Api.Interfaces;

public interface IFileStorageService
{
    Task<(bool Success, string? ErrorMessage, UploadResponseDto? Result)> UploadImageAsync(IFormFile file);
}
