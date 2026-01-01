using Microsoft.AspNetCore.Mvc;

namespace wu25_auction.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    
    public ExceptionHandlingMiddleware(
        RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (KeyNotFoundException ex)
        {
            await WriteProblemDetailsAsync(
                context, StatusCodes.Status404NotFound, "Resource not found", ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            await WriteProblemDetailsAsync(
                context, StatusCodes.Status400BadRequest, "Invalid operation", ex.Message);
        }
        catch (ArgumentException ex)
        {
            await WriteProblemDetailsAsync(
                context, StatusCodes.Status400BadRequest, "Invalid argument", ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            await WriteProblemDetailsAsync(
                context, StatusCodes.Status500InternalServerError, "Internal server error", "Unexpected server error");
        }
    }

    private static async Task WriteProblemDetailsAsync(
        HttpContext context, int statusCode, string title, string detail)
    {
        var problem = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Instance = context.Request.Path
        };
        
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/problem+json";
        
        await context.Response.WriteAsJsonAsync(problem);
    }
}