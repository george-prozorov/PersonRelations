using FluentValidation.AspNetCore;
using PersonReplations.Application;
using PersonReplations.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
  options.Filters.Add<ValidationFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
  .ConfigureApplicationServices()
  .ConfigurePersistenceServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
