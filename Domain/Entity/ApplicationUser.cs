using Microsoft.AspNetCore.Identity;

namespace Domain.Entity;

public class ApplicationUser : IdentityUser
{
    public required string Firstname { get; set; }

    public required string Lastname { get; set; }
}