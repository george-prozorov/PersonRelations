using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PersonReplations.Application
{
  public static class ApplicationRegisterServices
  {
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
      return services;
    }
  }
}
