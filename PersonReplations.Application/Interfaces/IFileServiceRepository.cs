using Microsoft.AspNetCore.Http;

namespace PersonReplations.Application.Interfaces;

public interface IFileServiceRepository
{
  Task<byte[]> GetFile(int personId, string fileName, CancellationToken cancellationToken = default);
  Task<string> SaveFileAsync(IFormFile file, int personId, CancellationToken cancellationToken = default);
}
