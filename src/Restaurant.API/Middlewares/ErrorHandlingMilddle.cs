using Microsoft.AspNetCore.Http.HttpResults;
using Restaurant.Domain.Exceptions;

namespace Restaurant.API.Middlewares
{
    public class ErrorHandlingMilddle(ILogger<ErrorHandlingMilddle> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotfoundException notfound)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notfound.Message);
                logger.LogWarning(notfound.Message);
            }
            catch (ForBidException)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Access forbidden");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("=Something went wrong");

            }
        }
    }
}
