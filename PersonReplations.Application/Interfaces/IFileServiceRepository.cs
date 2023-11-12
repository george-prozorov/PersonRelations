using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonReplations.Application.Interfaces
{
  public interface IFileServiceRepository
  {
    Task<byte[]> GetFile(int personId, string fileName);
    Task<string> SaveFileAsync(IFormFile file, int personId);
  }
}
