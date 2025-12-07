using System.Text.Json;
using Domain.Exceptions;

namespace Auth.MIddleware;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

 
    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var (statusCode, error) = ex switch
        {
            GlobalExceptions globalExceptions => (globalExceptions.StatusCode, new ErrorDetails
            {
                ErrorType = globalExceptions.ErrorType ?? "Application Error",
                Message = globalExceptions.Message,

            })
        };

        var response = ResponseModel<object>.ErrorResponse([error]);
        response.StatusCode = statusCode;
        response.TraceId = context.TraceIdentifier;

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });
    }
}