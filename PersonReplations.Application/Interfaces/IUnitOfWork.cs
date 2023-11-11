using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonReplations.Application.Interfaces
{
  public interface IUnitOfWork : IDisposable
  {
    IPersonRepository personRepository { get; }
    Task SaveChangesAsync();
  }
}
