using System.Text.Json.Serialization;

namespace Domain.Exceptions;

public class ErrorDetails
{
    public string? ErrorType { get; set; }

    public string? Message { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Field { get; set; }
}