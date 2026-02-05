using FunkoWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace FunkoWeb.Controllers;

/// <summary>
/// Controller for handling error pages
/// </summary>
public class ErrorController : Controller
{
    /// <summary>
    /// Handles error display based on status code
    /// </summary>
    [Route("Error/HandleError")]
    public IActionResult HandleError(int statusCode)
    {
        // Retrieve the error response from HttpContext.Items
        var errorResponse = HttpContext.Items["ErrorResponse"] as ErrorResponse;
        
        if (errorResponse == null)
        {
            // Fallback error response
            errorResponse = new ErrorResponse
            {
                StatusCode = statusCode,
                Message = "Ha ocurrido un error",
                RequestId = HttpContext.TraceIdentifier,
                Timestamp = DateTime.UtcNow,
                Path = HttpContext.Request.Path
            };
        }

        // Return appropriate view based on status code
        var viewName = statusCode switch
        {
            404 => "~/Views/Shared/404.cshtml",
            500 => "~/Views/Shared/500.cshtml",
            _ when statusCode >= 500 => "~/Views/Shared/500.cshtml",
            _ => "~/Views/Shared/Error.cshtml"
        };

        return View(viewName, errorResponse);
    }
}
