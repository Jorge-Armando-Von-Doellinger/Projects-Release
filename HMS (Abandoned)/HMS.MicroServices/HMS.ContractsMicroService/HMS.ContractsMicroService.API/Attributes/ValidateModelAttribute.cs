using HMS.ContractsMicroService.Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HMS.ContractsMicroService.API.Attributes
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var errors = new List<string>();
            foreach (var argument in context.ActionArguments)
            {
                var model = argument.Value;
                if (model == null) continue;
                Console.WriteLine(model.GetType().GetProperties().Length);
                Console.WriteLine(model.HaveAPropertyDefault(out var d));
                if (model.HaveAPropertyDefault(out var nameOfPropertiesDefault))
                {
                    errors.AddRange(nameOfPropertiesDefault.Select((nameOfProperty) =>
                    {
                        return $"{nameOfProperty} inválido!";
                    }));
                }
            }
            if (errors.Count > 0)
            {
                context.Result = new BadRequestObjectResult(errors);
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}
