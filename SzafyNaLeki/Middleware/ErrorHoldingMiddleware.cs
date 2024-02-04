
namespace SzafyNaLeki.Middleware
{
    public class ErrorHoldingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHoldingMiddleware> _logger;

        public ErrorHoldingMiddleware(ILogger<ErrorHoldingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception e) 
            {
                _logger.LogError(e, e.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Coś poszło nie tak");
            }
        }
    }


}
