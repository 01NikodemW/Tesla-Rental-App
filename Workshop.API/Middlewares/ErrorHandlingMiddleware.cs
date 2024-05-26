using Workshop.Domain.Exceptions;

namespace Workshop.API.Middlewares;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NotFoundException notFound)
        {
            logger.LogError(notFound.Message);
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(notFound.Message);
        }
        catch (ForbidException forbid)
        {
            logger.LogError(forbid.Message);
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Access forbidden");
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Something went wrong");
        }
    }
}