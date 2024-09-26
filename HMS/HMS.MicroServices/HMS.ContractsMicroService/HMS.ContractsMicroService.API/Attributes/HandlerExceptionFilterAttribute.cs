using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HMS.ContractsMicroService.API.Attributes
{
    public class HandlerExceptionFilterAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if(context.Exception == null) return;
            context.Result = new BadRequestObjectResult(new { Message = context.Exception.Message } );
            context.ExceptionHandled = true;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            return;
        }
    }
}
