using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PersonReplations.Application;

public class ValidationFilter : IActionFilter
{
  private readonly IServiceProvider _serviceProvider;
  public ValidationFilter(IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }
  public void OnActionExecuted(ActionExecutedContext context) { }

  public void OnActionExecuting(ActionExecutingContext context)
  {
    var request = context.ActionArguments.FirstOrDefault().Value;
    if (request == null) return;

    var requestType = request.GetType();
    var validatorType = typeof(IValidator<>).MakeGenericType(requestType);
    object? service = _serviceProvider.GetService(validatorType);
    IValidator validator;
    if (service is IValidator validatorService)
      validator = validatorService;
    else return;

    var contextType = typeof(ValidationContext<>).MakeGenericType(requestType);
    var validationContext = Activator.CreateInstance(contextType, request);

    var validationResult = validator.Validate(validationContext as IValidationContext);

    if (!validationResult.IsValid)
    {
      context.Result = new BadRequestObjectResult(
        validationResult.Errors.Select(e => e.ErrorMessage));
    }

  }
}
