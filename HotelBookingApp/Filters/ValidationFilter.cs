using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HotelBookingApp.Filters;

public class ValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        foreach (var argument in context.ActionArguments.Values)
        {
            if (argument == null) continue;

            var validationResult = await ValidateAsync(argument, context);
            if (validationResult != null && !validationResult.IsValid)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    errors = validationResult.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(e => e.ErrorMessage).ToArray()
                        )
                });
                return;
            }
        }

        await next();
    }

    private static async Task<FluentValidation.Results.ValidationResult?> ValidateAsync(object instance, ActionExecutingContext context)
    {
        var validatorType = typeof(IValidator<>).MakeGenericType(instance.GetType());
        var validator = context.HttpContext.RequestServices.GetService(validatorType) as IValidator;

        if (validator == null) return null;

        var method = validatorType.GetMethod("ValidateAsync", new[] { instance.GetType(), typeof(CancellationToken) });
        if (method == null) return null;

        var task = (Task)method.Invoke(validator, new object[] { instance, CancellationToken.None })!;
        await task.ConfigureAwait(false);

        var resultProperty = task.GetType().GetProperty("Result");
        return resultProperty?.GetValue(task) as FluentValidation.Results.ValidationResult;
    }
}
