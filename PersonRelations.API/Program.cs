using Microsoft.Extensions.Options;
using PersonRelations.API.Middlewares;
using PersonReplations.Application;
using PersonReplations.Persistence;
using Serilog;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(options =>
{
  options.Filters.Add<ValidationFilter>();
});

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings)));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<Stopwatch>();

builder.Services
  .ConfigureApplicationServices()
  .ConfigurePersistenceServices(builder.Configuration);

builder.Host.UseSerilog((context, configuration) =>
  configuration.ReadFrom.Configuration(context.Configuration));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

var localizeOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();

app.UseRequestLocalization(localizeOptions!.Value);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ErrorHandling>();

app.Run();
