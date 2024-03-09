using Microsoft.AspNetCore.Mvc.Filters;

namespace BigMarket.Services.CouponAPI.Filters.ActionFilters
{
    public class ResponseHeaderActionFilter(ILogger<ResponseHeaderActionFilter> logger,
                                                                        string key,
                                                                        string value) : IActionFilter
    {
        ILogger<ResponseHeaderActionFilter> _logger = logger;
        private readonly string _key = key;
        private readonly string _value = value;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // To do: add after logic here
            _logger.LogInformation($"{nameof(ResponseHeaderActionFilter)}.{nameof(OnActionExecuted)} method");
            context.HttpContext.Response.Headers[_key] = _value;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //To do: add before logic here
            _logger.LogInformation($"{nameof(ResponseHeaderActionFilter)}.{nameof(OnActionExecuting)} method");
        }
    }
}
