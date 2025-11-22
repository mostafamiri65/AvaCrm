using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AvaCrm.Application.Contracts;
public interface IFileValidator
{
    Task<FileValidationResult> ValidateAsync(IFormFile file);
    IEnumerable<string> GetSupportedFileTypes();
}

public class FileValidator : IFileValidator
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<FileValidator> _logger;

    public FileValidator(IConfiguration configuration, ILogger<FileValidator> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<FileValidationResult> ValidateAsync(IFormFile file)
    {
        var errors = new List<string>();

        if (file == null || file.Length == 0)
        {
            errors.Add("File is empty");
            return new FileValidationResult { IsValid = false, Errors = errors };
        }

        // Check file size
        var maxFileSize = Convert.ToInt64(_configuration["FileUpload:MaxFileSize"]); 
        if (file.Length > maxFileSize)
        {
            errors.Add($"File size exceeds the maximum allowed size of {maxFileSize / (1024 * 1024)}MB");
        }

        // Check file extension
        var allowedExtensions = GetSupportedFileTypes();
        var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (!allowedExtensions.Contains(fileExtension))
        {
            errors.Add($"File type {fileExtension} is not supported. Allowed types: {string.Join(", ", allowedExtensions)}");
        }

        // Check content type
        var allowedContentTypes = GetSupportedContentTypes();
        if (!allowedContentTypes.Contains(file.ContentType.ToLowerInvariant()))
        {
            errors.Add($"Content type {file.ContentType} is not supported");
        }

        // Additional security checks
        await CheckFileSignatureAsync(file, errors);

        return new FileValidationResult
        {
            IsValid = !errors.Any(),
            Errors = errors
        };
    }

    public IEnumerable<string> GetSupportedFileTypes()
    {
        var extensions = _configuration.GetSection("FileUpload:AllowedExtensions").Get<IEnumerable<string>>();

        return extensions?.Any() == true
            ? extensions
            : new[] { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".doc", ".docx", ".txt" };
    }

    private IEnumerable<string> GetSupportedContentTypes()
    {
        return _configuration.GetSection("FileUpload:AllowedContentTypes").Get<string[]>()
            ?? new[] { "image/jpeg", "image/png", "image/gif", "application/pdf", "application/msword", "text/plain" };
    }

    private async Task CheckFileSignatureAsync(IFormFile file, List<string> errors)
    {
        try
        {
            using var stream = file.OpenReadStream();
            var buffer = new byte[20];
            await stream.ReadAsync(buffer, 0, buffer.Length);

            // Reset stream position for actual upload
            stream.Position = 0;

            // Simple file signature validation (expand based on your needs)
            if (!IsValidFileSignature(buffer, Path.GetExtension(file.FileName)))
            {
                errors.Add("File signature does not match the file extension");
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Error during file signature validation for {FileName}", file.FileName);
            errors.Add("Unable to validate file signature");
        }
    }

    private bool IsValidFileSignature(byte[] buffer, string fileExtension)
    {
        // Implement file signature validation logic here
        // This is a simplified example
        return fileExtension.ToLowerInvariant() switch
        {
            ".jpg" or ".jpeg" => buffer[0] == 0xFF && buffer[1] == 0xD8,
            ".png" => buffer[0] == 0x89 && buffer[1] == 0x50,
            ".pdf" => buffer[0] == 0x25 && buffer[1] == 0x50,
            _ => true // For other types, you might want more specific validation
        };
    }
}

public record FileValidationResult
{
    public bool IsValid { get; init; }
    public IEnumerable<string> Errors { get; init; } = Enumerable.Empty<string>();
}