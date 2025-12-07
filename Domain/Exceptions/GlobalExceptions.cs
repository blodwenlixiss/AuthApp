using System.Diagnostics;

namespace Domain.Exceptions;

public class GlobalExceptions : Exception
{
    public int StatusCode { get; }
    public string? ErrorType { get; }

    public GlobalExceptions(string message, int statusCode, string? errorType = null)
        : base(message)
    {
        StatusCode = statusCode;
        ErrorType = errorType ?? DetermineErrorType(statusCode);
    
    }

    private static string DetermineErrorType(int statusCode)
    {
        return statusCode switch
        {
            400 => "Bad Request",
            401 => "Unauthorized",
            403 => "Forbidden",
            404 => "Not Found",
            409 => "Conflict",
            422 => "Validation Error",
            _ => "Application Error"
        };
    }
}