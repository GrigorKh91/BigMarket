using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;

namespace BigMarket.Services.CouponAPI.Filters.ResultFilters
{
    public class CouponListResultFilter(ILogger<CouponListResultFilter> logger) : IAsyncResultFilter
    {
        private readonly ILogger<CouponListResultFilter> _logger = logger;

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            //TO DO: before logic
            _logger.LogInformation("{FilterName}.{MethodName} - before", nameof(CouponListResultFilter), nameof(OnResultExecutionAsync));

            await next(); //call the subsequent filter [or] IActionResult

            //TO DO: after logic
            _logger.LogInformation("{FilterName}.{MethodName} - after", nameof(CouponListResultFilter), nameof(OnResultExecutionAsync));
           
        }
    }
}