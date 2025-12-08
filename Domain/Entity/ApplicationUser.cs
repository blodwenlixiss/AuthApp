using Microsoft.AspNetCore.Identity;

namespace Domain.Entity;

public class ApplicationUser : IdentityUser
{
    public required string Firstname { get; set; }

    public required string Lastname { get; set; }


    public string? RefreshToken { get; set; }

    public DateTime RefreshTokenExpireDate { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}