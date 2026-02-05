using System.Diagnostics;
using System.Net;
using FunkoWeb.Exceptions;
using FunkoWeb.Models;

namespace FunkoWeb.Middleware;

/// <summary>
/// Global exception handler middleware that catches all unhandled exceptions
/// </summary>
public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
    private readonly IWebHostEnvironment _environment;

    public GlobalExceptionHandlerMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionHandlerMiddleware> logger,
        IWebHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
            
            // Handle 404 Not Found for requests that don't match any endpoint
            if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
            {
                await HandleNotFoundAsync(context);
            }
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var requestId = Activity.Current?.Id ?? context.TraceIdentifier;
        
        // Log the exception
        _logger.LogError(exception, 
            "Unhandled exception occurred. RequestId: {RequestId}, Path: {Path}", 
            requestId, context.Request.Path);

        // Determine status code and message based on exception type
        var (statusCode, message) = exception switch
        {
            AppException appEx => ((int)appEx.StatusCode, appEx.Message),
            UnauthorizedAccessException => ((int)HttpStatusCode.Forbidden, "No tienes permisos para acceder a este recurso"),
            _ => ((int)HttpStatusCode.InternalServerError, "Ha ocurrido un error interno en el servidor")
        };

        // Create error response
        var errorResponse = new ErrorResponse
        {
            StatusCode = statusCode,
            Message = message,
            RequestId = requestId,
            Timestamp = DateTime.UtcNow,
            Path = context.Request.Path
        };

        // Add detailed information in Development environment
        if (_environment.IsDevelopment())
        {
            errorResponse.Details = new ErrorDetails
            {
                ExceptionType = exception.GetType().Name,
                StackTrace = exception.StackTrace,
                InnerException = exception.InnerException?.Message,
                Source = exception.Source
            };
        }

        context.Response.StatusCode = statusCode;

        // Check if this is an API request (JSON) or a web request (HTML)
        var acceptHeader = context.Request.Headers["Accept"].ToString();
        var isApiRequest = acceptHeader.Contains("application/json", StringComparison.OrdinalIgnoreCase) ||
                          context.Request.Path.StartsWithSegments("/api");

        if (isApiRequest)
        {
            // Return JSON response for API requests
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
        else
        {
            // Return HTML view for web requests
            await RenderErrorPage(context, errorResponse);
        }
    }

    private async Task HandleNotFoundAsync(HttpContext context)
    {
        var requestId = Activity.Current?.Id ?? context.TraceIdentifier;
        
        _logger.LogWarning("404 Not Found. RequestId: {RequestId}, Path: {Path}", 
            requestId, context.Request.Path);

        var errorResponse = new ErrorResponse
        {
            StatusCode = 404,
            Message = "La página o recurso que buscas no existe",
            RequestId = requestId,
            Timestamp = DateTime.UtcNow,
            Path = context.Request.Path
        };

        await RenderErrorPage(context, errorResponse);
    }

    private async Task RenderErrorPage(HttpContext context, ErrorResponse errorResponse)
    {
        // Determine which view to render based on status code
        var viewName = errorResponse.StatusCode switch
        {
            404 => "404",
            500 => "500",
            _ when errorResponse.StatusCode >= 500 => "500",
            _ => "Error"
        };

        // Store the error response in HttpContext.Items for the view to access
        context.Items["ErrorResponse"] = errorResponse;

        // Re-execute to the error handling endpoint
        var originalPath = context.Request.Path;
        var originalQueryString = context.Request.QueryString;
        
        context.Request.Path = "/Error/HandleError";
        context.Request.QueryString = new QueryString($"?statusCode={errorResponse.StatusCode}");
        
        try
        {
            await _next(context);
        }
        finally
        {
            context.Request.Path = originalPath;
            context.Request.QueryString = originalQueryString;
        }
    }
}
