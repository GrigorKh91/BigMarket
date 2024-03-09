using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BigMarket.Services.CouponAPI.Filters.ExceptionFilters
{
    public class HandleExceptionFilter(ILogger<HandleExceptionFilter> logger, IHostEnvironment hostEvironment) : IExceptionFilter
    {
        private readonly ILogger<HandleExceptionFilter> _logger = logger;
        private readonly IHostEnvironment _hostEnvironment = hostEvironment;

        public void OnException(ExceptionContext context)
        {
            _logger.LogError("Exception filter {FilterName}.{MethodName}\n{ExceptionType}\n{ExceptionMessage}", nameof(HandleExceptionFilter), 
                nameof(OnException), context.Exception.GetType().ToString(), context.Exception.Message);

            if (_hostEnvironment.IsDevelopment())
                context.Result = new ContentResult() { Content = context.Exception.Message, StatusCode = 500 };
        }
    }

}
