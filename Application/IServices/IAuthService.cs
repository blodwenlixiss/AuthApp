using Application.Dtos;

namespace Application.IServices;

public interface IAuthService
{
    Task CreateUserAsync(RegisterDto registerDto);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
    Task Logout();
}