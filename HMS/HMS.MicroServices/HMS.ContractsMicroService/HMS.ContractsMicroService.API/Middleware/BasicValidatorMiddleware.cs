using ZstdSharp.Unsafe;

namespace HMS.ContractsMicroService.API.Middleware
{
    public class BasicValidatorMiddleware
    {
        private readonly RequestDelegate _next;

        public BasicValidatorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public void Invoke(HttpContext context)
        {
            if(context.Request.Method == HttpMethod.Get.Method && context.Request.Query != null)
            {
                var query = context.Request.Query;
                
            }
        }
    }
}
