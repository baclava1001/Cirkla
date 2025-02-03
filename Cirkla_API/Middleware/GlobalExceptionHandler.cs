using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Cirkla_API.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }


    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);
        
        var details = new ProblemDetails()
        {
            Title = "An API error occurred",
            Instance = "API",
            Type = "Server Error",
            Status = StatusCodes.Status500InternalServerError,
            Detail = exception.Message
        };

        var response = JsonSerializer.Serialize(details);
        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsync(response, cancellationToken);
        return true;
    }
}