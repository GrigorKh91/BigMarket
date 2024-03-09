using Microsoft.AspNetCore.Mvc.Filters;

namespace BigMarket.Services.CouponAPI.Filters.ActionFilters
{
    public class CouponCreateFilter(ILogger<CouponCreateFilter> logger) : IActionFilter
    {
        ILogger<CouponCreateFilter> _logger = logger;
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //To do: add after logic here
            _logger.LogInformation($"{nameof(CouponCreateFilter)}.{nameof(OnActionExecuted)} method");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //To do: add before logic here
            _logger.LogInformation($"{nameof(CouponCreateFilter)}.{nameof(OnActionExecuting)} method");
        }
    }
}
