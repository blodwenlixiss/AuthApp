using Application.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Application.IServices;

public interface IAuthService
{
    Task CreateUserAsync(RegisterDto registerDto);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<(bool Success, AuthResponseDto? Response, string Message)> LoginAsync(LoginDto loginDto);
    Task<(bool Success, AuthResponseDto? Response, string Message)> RefreshTokenAsync(TokenDto tokenDto);
    Task<(bool Success, string Message)> LogoutAsync(string userId);
}