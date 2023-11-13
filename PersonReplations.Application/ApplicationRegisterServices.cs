using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Reflection;

namespace PersonReplations.Application;

public static class ApplicationRegisterServices
{
  public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
  {
    var thisAssembly = Assembly.GetExecutingAssembly();
    services.AddMediatR(o => o.RegisterServicesFromAssemblies(thisAssembly));
    services.AddValidatorsFromAssembly(thisAssembly);
    services.AddAutoMapper(thisAssembly);
    services.AddLocalization();
    services.Configure<RequestLocalizationOptions>(options =>
    {
      var supportedCultures = new List<CultureInfo>
      {
        new CultureInfo("ka-GE"),
        new CultureInfo("en-US")
      };
      options.DefaultRequestCulture = new RequestCulture(supportedCultures[0]);
      options.SupportedCultures = supportedCultures;
      options.SupportedUICultures = supportedCultures;
    });
    return services;
  }
}
