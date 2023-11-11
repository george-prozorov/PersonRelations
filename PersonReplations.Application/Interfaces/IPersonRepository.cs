using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonReplations.Application.Interfaces
{
  public interface IPersonRepository
  {
    Task AddAsync<T>(T entity) where T : class;
    Task<T?> GetByIdAsync<T>(int id) where T : class;
  }
}
