namespace Domain.Exceptions;

public class NotFoundException : GlobalExceptions
{
    public NotFoundException(string message)
        : base(message, 404, "Not Found")
    {
    }
}

public class BadRequestException : GlobalExceptions
{
    public BadRequestException(string message)
        : base(message, 400, "Bad Request")
    {
    }
}

public class UnauthorizedException : GlobalExceptions
{
    public UnauthorizedException(string message)
        : base(message, 401, "Unauthorized")
    {
    }
}

public class ForbiddenException : GlobalExceptions
{
    public ForbiddenException(string message)
        : base(message, 403, "Forbidden")
    {
    }
}

public class ConflictException : GlobalExceptions
{
    public ConflictException(string message)
        : base(message, 409, "Conflict")
    {
    }
}

public class ValidationException : GlobalExceptions
{
    public ValidationException(string message)
        : base(message, 422, "Validation Error")
    {
    }
}