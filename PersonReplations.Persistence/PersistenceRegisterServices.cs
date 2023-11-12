using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonReplations.Application.Interfaces;
using PersonReplations.Persistence.Repositories;

namespace PersonReplations.Persistence;

public static class PersistenceRegisterServices
{
  public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, ConfigurationManager configuration)
  {
    services.AddDbContext<PersonRelationsDbContext>(options =>
              options.UseSqlServer(configuration.GetConnectionString("Default"),
                  b => b.MigrationsAssembly(typeof(PersonRelationsDbContext).Assembly.FullName)));
    services.AddScoped<IUnitOfWork, UnitOfWork>();
    services.AddScoped<IPersonRepository, PersonRepository>();
    services.AddScoped<IReferenceRepository, ReferenceRepository>();
    services.AddMemoryCache();
    return services;
  }
}
