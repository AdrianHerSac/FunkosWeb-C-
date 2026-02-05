namespace FunkoWeb.Models;

/// <summary>
/// Standardized error response model
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// HTTP status code
    /// </summary>
    public int StatusCode { get; set; }
    
    /// <summary>
    /// User-friendly error message
    /// </summary>
    public string Message { get; set; } = string.Empty;
    
    /// <summary>
    /// Request ID for tracking and logging
    /// </summary>
    public string RequestId { get; set; } = string.Empty;
    
    /// <summary>
    /// Timestamp when the error occurred
    /// </summary>
    public DateTime Timestamp { get; set; }
    
    /// <summary>
    /// Path where the error occurred
    /// </summary>
    public string Path { get; set; } = string.Empty;
    
    /// <summary>
    /// Detailed error information (only in Development environment)
    /// </summary>
    public ErrorDetails? Details { get; set; }
    
    /// <summary>
    /// Helper property for views to display Request ID conditionally
    /// </summary>
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
