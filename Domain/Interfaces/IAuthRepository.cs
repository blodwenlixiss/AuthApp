using Domain.Entity;
using Microsoft.AspNetCore.Identity;

namespace Domain.Interfaces;

public interface IAuthRepository
{
    Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
    Task<IEnumerable<RefreshToken>?> GetRefreshTokenFromUserId(string userId);
    Task CreateRefreshTokenAsync(RefreshToken refreshToken);
    Task<RefreshToken?> GetRefreshTokenAsync(string token);
}