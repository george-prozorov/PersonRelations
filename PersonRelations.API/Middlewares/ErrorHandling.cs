using System.Diagnostics;

namespace PersonRelations.API.Middlewares;


public class ErrorHandling
{
  private readonly RequestDelegate _next;
  private readonly Serilog.ILogger _logger;
  private readonly Stopwatch _stopwatch;
  public ErrorHandling(RequestDelegate next, Serilog.ILogger logger, Stopwatch stopwatch)
  {
    _next = next;
    _logger = logger;
    _stopwatch = stopwatch;
  }
  public async Task InvokeAsync(HttpContext context)
  {
    _stopwatch.Start();
    try
    {
      await _next(context);
    }
    catch (Exception ex)
    {
#if DEBUG
      var errorMessage = new { ex.Message };
#else
        var errorMessage = new { Message = $"Error proccessing request, please contact support! TraceId: context.TraceIdentifier" };
#endif
      _stopwatch.Stop();
      _logger.Error(ex, "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000}",
        context.Request.Method,
        context.Request.Path,
        "(500) Internal Server Error",
        _stopwatch.ElapsedMilliseconds);
      context.Response.StatusCode = 500;
      context.Response.Headers.Add("Traceid", context.TraceIdentifier);
      await context.Response.WriteAsJsonAsync(errorMessage);
    }
  }
}
