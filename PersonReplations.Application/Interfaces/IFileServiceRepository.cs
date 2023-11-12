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
    Task<string> SaveFileAsync(IFormFile file, int personId);
  }
}
