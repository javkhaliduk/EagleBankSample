using EagleBank.Models.common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EagleBank.filters
{
    public class CustomValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var details = context.ModelState
                    .Where(ms => ms.Value.Errors.Count > 0)
                    .Select(ms => new ErrorDetails
                    {
                        Field = ms.Key,
                        Message = ms.Value.Errors.First().ErrorMessage,
                        Type = "validation" // Or derive from error type/context
                    })
                    .ToList();

                var errorResponse = new ErrorResponse
                {
                    Message = "The request didn't supply all the necessary data",
                    Details = details
                };

                context.Result = new BadRequestObjectResult(errorResponse);
            }
        }
    }
}
