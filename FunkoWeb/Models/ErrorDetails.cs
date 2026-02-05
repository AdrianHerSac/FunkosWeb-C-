namespace FunkoWeb.Models;

/// <summary>
/// Detailed error information for debugging (Development environment only)
/// </summary>
public class ErrorDetails
{
    /// <summary>
    /// Exception type name
    /// </summary>
    public string ExceptionType { get; set; } = string.Empty;
    
    /// <summary>
    /// Stack trace
    /// </summary>
    public string? StackTrace { get; set; }
    
    /// <summary>
    /// Inner exception message if present
    /// </summary>
    public string? InnerException { get; set; }
    
    /// <summary>
    /// Source of the exception
    /// </summary>
    public string? Source { get; set; }
}
