using Microsoft.AspNetCore.Mvc.Filters;

namespace Contacts_Manager.Filters.ActionFilters
{
    public class ResponseHeaderActionFilter : IAsyncActionFilter, IOrderedFilter
    {
        private readonly ILogger<ResponseHeaderActionFilter> _logger;
        private readonly string _key;
        private readonly string _value;
        public int Order { get; set; }

        public ResponseHeaderActionFilter(ILogger<ResponseHeaderActionFilter> logger,
            string key,
            string value,
            int order)
        {
           _logger = logger;
           _key = key;
           _value = value;
           Order = order;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // code from OnActionExecuting()
            // structured serilog logging
            _logger.LogInformation("{FilterName}.{MethodName} before method", nameof(ResponseHeaderActionFilter), nameof(OnActionExecutionAsync));


            await next(); // calls the subsequent filter or action method



            // code from OnActionExecuted()
            context.HttpContext.Response.Headers[_key] = _value;
            // structured serilog logging
            _logger.LogInformation("{FilterName}.{MethodName} after method", nameof(ResponseHeaderActionFilter), nameof(OnActionExecutionAsync));
        }
    }
}
