using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PersonReplations.Application;

public class ValidationFilter : IAsyncActionFilter
{
  private readonly IServiceProvider _serviceProvider;

  public ValidationFilter(IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
  {
    var request = context.ActionArguments.FirstOrDefault().Value;
    if (request == null) { await next(); return; }

    var requestType = request.GetType();
    var validatorType = typeof(IValidator<>).MakeGenericType(requestType);
    object? service = _serviceProvider.GetService(validatorType);
    IValidator validator;
    if (service is IValidator validatorService)
      validator = validatorService;
    else { await next(); return; }

    var contextType = typeof(ValidationContext<>).MakeGenericType(requestType);
    var validationContext = Activator.CreateInstance(contextType, request);

    var validationResult = await validator.ValidateAsync(validationContext as IValidationContext);

    if (!validationResult.IsValid)
    {
      context.Result = new BadRequestObjectResult(
        validationResult.Errors.Select(e => e.ErrorMessage));
    }
    else
      await next();
  }
}
