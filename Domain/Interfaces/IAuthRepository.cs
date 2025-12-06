using Domain.Entity;
using Microsoft.AspNetCore.Identity;

namespace Domain.Interfaces;

public interface IAuthRepository
{
    Task CreateUserAsync(ApplicationUser user, string password);
    Task<ApplicationUser?> FindUserByEmailAsync(string email);

    Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
    Task<ApplicationUser?> FindUserByIdAsync(string userId);
    Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
    Task<IdentityResult> UpdateUserAsync(ApplicationUser user);
}