using System.Text.Json.Serialization;

namespace Domain.Exceptions;

public class ResponseModel<T>
{
    public int StatusCode { get; set; }

    public bool Success { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Data { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<ErrorDetails>? Errors { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? TraceId { get; set; }

    public DateTime TimeStamp { get; set; }

    public static ResponseModel<T> SuccessResponse(T? data =default, int statusCode=200)
    {
        return new ResponseModel<T>
        {
            StatusCode = statusCode,
            Success = true,
            Data = data,
            TimeStamp = DateTime.UtcNow,
        };
    }
    
    public static ResponseModel<T> ErrorResponse(List<ErrorDetails>? errorDetails, int statusCode = 500){
        return new ResponseModel<T>
        {
            StatusCode = statusCode,
            Success = false,
            Errors = errorDetails,
            TimeStamp = DateTime.UtcNow,
        };
    }
}