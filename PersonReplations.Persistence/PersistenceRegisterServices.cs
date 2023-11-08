using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonReplations.Persistence
{
  public static class PersistenceRegisterServices
  {
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services)
    {
      return services;
    }
  }
}
