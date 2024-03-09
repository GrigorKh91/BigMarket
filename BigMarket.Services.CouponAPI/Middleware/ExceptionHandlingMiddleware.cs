using BigMarket.Services.CouponAPI.Models.Dto;

namespace BigMarket.Services.CouponAPI.Middleware
{
    public class ExceptionHandlingMiddleware(RequestDelegate next,
                                                                           ILogger<ExceptionHandlingMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;
        private readonly ResponseDto _response = new();
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    _logger.LogError("{ExceptionType} {ExceptionMessage}", ex.InnerException.GetType().ToString(), ex.InnerException.Message);
                }
                else
                {
                    _logger.LogError("{ExceptionType} {ExceptionMessage}", ex.GetType().ToString(), ex.Message);
                }

                _response.IsSuccess = false;
                _response.Message = "Error occurred";

                httpContext.Response.StatusCode =
                    StatusCodes.Status500InternalServerError;

                await httpContext.Response.WriteAsJsonAsync(_response);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }

}
