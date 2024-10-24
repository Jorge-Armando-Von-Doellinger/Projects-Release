using HMS.ContractsMicroService.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace HMS.ContractsMicroService.API.Attributes
{
    public class ValidateJsonBySchemaAttribute : Attribute, IAsyncActionFilter
    {
        private readonly ISchemaFilter _schemaFilter;

        public ValidateJsonBySchemaAttribute(ISchemaFilter schema)
        {
            _schemaFilter = schema;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.Request.Method != "GET")
            {
                context.HttpContext.Request.EnableBuffering();
                using (var reader = new StreamReader(context.HttpContext.Request.Body, Encoding.UTF8, true))
                {
                    var body = await reader.ReadToEndAsync();
                    int errors = _schemaFilter.Schema.Validate(body).Count;
                    if (errors > 0)
                        context.Result = new BadRequestObjectResult("Json recieved isn't valid!");
                    context.HttpContext.Response.Body.Position = 0;
                }
            }
            await next();
        }
    }
}
