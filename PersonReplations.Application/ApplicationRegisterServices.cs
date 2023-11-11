using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace PersonReplations.Application;

public static class ApplicationRegisterServices
{
  public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
  {
    var thisAssembly = Assembly.GetExecutingAssembly();
    services.AddMediatR(o => o.RegisterServicesFromAssemblies(thisAssembly));
    services.AddValidatorsFromAssembly(thisAssembly);
    return services;
  }
}
