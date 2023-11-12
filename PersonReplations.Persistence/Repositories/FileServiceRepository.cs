using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Options;
using PersonReplations.Application;
using PersonReplations.Application.Interfaces;

namespace PersonReplations.Persistence.Repositories;

public class FileServiceRepository : IFileServiceRepository
{
  private readonly AppSettings _appSettings;
  public FileServiceRepository(IOptions<AppSettings> options)
  {
    _appSettings = options.Value;
  }

  public Task<byte[]> GetFile(int personId, string fileName)
  {
    var path = Path.Combine(_appSettings.ImageFolder, personId.ToString(), fileName);
    return File.ReadAllBytesAsync(path);
  }

  public async Task<string> SaveFileAsync(IFormFile file, int personId)
  {

    var fileName = Guid.NewGuid().ToString() + file.ContentType switch
    {
      "image/jpeg" => ".jpeg",
      "image/jpg" => ".jpg",
      "image/png" => ".png",
      _ => file.FileName
    };
    var folderPath = Path.Combine(_appSettings.ImageFolder, personId.ToString());
    if (!Directory.Exists(folderPath))
    {
      Directory.CreateDirectory(folderPath);
    }
    string path = Path.Combine(folderPath, fileName);
    using (var stream = new FileStream(path, FileMode.Create))
    {
      await file.CopyToAsync(stream);
    }
    return fileName;
  }
}
