using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PersonReplations.Persistence;

public static class PersistenceRegisterServices
{
  public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, ConfigurationManager configuration)
  {
    services.AddDbContext<PersonRelationsDbContext>(options =>
              options.UseSqlServer(configuration.GetConnectionString("Default"),
                  b => b.MigrationsAssembly(typeof(PersonRelationsDbContext).Assembly.FullName)));
    return services;
  }
}
