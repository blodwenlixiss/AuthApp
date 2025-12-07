using Domain.Entity;

namespace Application.Dtos;

public class AuthResponseDto
{ 
    public required string UserId { get; set; }
    
    public required string  FirstName { get; set; }
    
    public required string LastName { get; set; }
    
    public required string Email { get; set; }
    public string AccessToken { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
}