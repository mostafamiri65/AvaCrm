using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AvaCrm.Application.Contracts;

public interface IFileStorage
{
    Task<string> SaveAsync(IFormFile file, string fileName, CancellationToken cancellationToken = default);
    Task<Stream> GetAsync(string fileName, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(string fileName, CancellationToken cancellationToken = default);
}


public class LocalFileStorage : IFileStorage
{
    private readonly string _uploadPath;
    private readonly ILogger<LocalFileStorage> _logger;

    public LocalFileStorage(IConfiguration configuration, ILogger<LocalFileStorage> logger)
    {
        _uploadPath = configuration["FileUpload:StoragePath"] ?? "wwwroot/uploads";
        _logger = logger;

        // Ensure upload directory exists
        Directory.CreateDirectory(_uploadPath);
    }

    public async Task<string> SaveAsync(IFormFile file, string fileName, CancellationToken cancellationToken = default)
    {
        var filePath = Path.Combine(_uploadPath, fileName);

        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream, cancellationToken);

        _logger.LogInformation("File saved to: {FilePath}", filePath);
        return filePath;
    }

    public Task<Stream> GetAsync(string fileName, CancellationToken cancellationToken = default)
    {
        var filePath = Path.Combine(_uploadPath, fileName);
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"File {fileName} not found");
        }

        var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        return Task.FromResult<Stream>(stream);
    }

    public Task<bool> DeleteAsync(string fileName, CancellationToken cancellationToken = default)
    {
        var filePath = Path.Combine(_uploadPath, fileName);

        if (!File.Exists(filePath))
        {
            return Task.FromResult(false);
        }

        File.Delete(filePath);
        _logger.LogInformation("File deleted: {FilePath}", filePath);
        return Task.FromResult(true);
    }
}