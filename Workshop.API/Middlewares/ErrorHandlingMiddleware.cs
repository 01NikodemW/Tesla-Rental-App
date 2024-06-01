using System.Text.Json;
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
            await WriteErrorResponseAsync(context, 404, "Not Found", notFound.Message);
        }
        catch (UnauthorizedException unauthorizedException)
        {
            logger.LogError("Invalid email or password");
            await WriteErrorResponseAsync(context, 401, "Unauthorized", "Invalid email or password");
        }
        catch (CarNotAvailableException carNotAvailable)
        {
            logger.LogError(carNotAvailable.Message);
            await WriteErrorResponseAsync(context, 400, "Bad Request", carNotAvailable.Message);
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            await WriteErrorResponseAsync(context, 500, "Internal Server Error", "Something went wrong");
        }
    }


    private async Task WriteErrorResponseAsync(HttpContext context, int statusCode, string title, string message)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var errorResponse = new
        {
            title,
            status = statusCode,
            message
        };

        var errorResponseJson = JsonSerializer.Serialize(errorResponse);
        await context.Response.WriteAsync(errorResponseJson);
    }
}