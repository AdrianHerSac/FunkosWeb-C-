using System.Net;

namespace FunkoWeb.Exceptions;

/// <summary>
/// Base class for all custom application exceptions
/// </summary>
public abstract class AppException : Exception
{
    public HttpStatusCode StatusCode { get; }
    
    protected AppException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) 
        : base(message)
    {
        StatusCode = statusCode;
    }
}

/// <summary>
/// Exception thrown when a requested resource is not found (404)
/// </summary>
public class NotFoundException : AppException
{
    public NotFoundException(string message = "El recurso solicitado no fue encontrado") 
        : base(message, HttpStatusCode.NotFound)
    {
    }
    
    public NotFoundException(string resourceType, object key) 
        : base($"{resourceType} con identificador '{key}' no fue encontrado", HttpStatusCode.NotFound)
    {
    }
}

/// <summary>
/// Exception thrown when the request is invalid (400)
/// </summary>
public class BadRequestException : AppException
{
    public BadRequestException(string message = "La solicitud es inválida") 
        : base(message, HttpStatusCode.BadRequest)
    {
    }
}

/// <summary>
/// Exception thrown when authentication is required (401)
/// </summary>
public class UnauthorizedException : AppException
{
    public UnauthorizedException(string message = "Autenticación requerida") 
        : base(message, HttpStatusCode.Unauthorized)
    {
    }
}

/// <summary>
/// Exception thrown when the user doesn't have permission (403)
/// </summary>
public class ForbiddenException : AppException
{
    public ForbiddenException(string message = "No tienes permisos para acceder a este recurso") 
        : base(message, HttpStatusCode.Forbidden)
    {
    }
}

/// <summary>
/// Exception thrown when there's a conflict with existing data (409)
/// </summary>
public class ConflictException : AppException
{
    public ConflictException(string message = "Ya existe un recurso con esos datos") 
        : base(message, HttpStatusCode.Conflict)
    {
    }
}
