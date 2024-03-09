using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BigMarket.Services.CouponAPI.Filters.ResourceFilters
{
    public class FeatureDisabledResourceFilter(ILogger<FeatureDisabledResourceFilter> logger,
                                                                            bool isDisabled = true) : IAsyncResourceFilter
    {
        private readonly ILogger<FeatureDisabledResourceFilter> _logger = logger;
        private readonly bool _isDisabled = isDisabled;

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            //TO DO: before logic
            _logger.LogInformation("{FilterName}.{MethodName} - before", nameof(FeatureDisabledResourceFilter), nameof(OnResourceExecutionAsync));

            if (_isDisabled)
            {
                //context.Result = new NotFoundResult(); //404 - Not Found

                context.Result = new StatusCodeResult(501); //501 - Not Implemented
            }
            else
            {
                await next();
            }

            //TO DO: after logic
            _logger.LogInformation("{FilterName}.{MethodName} - after", nameof(FeatureDisabledResourceFilter), nameof(OnResourceExecutionAsync));
        }
    }
}