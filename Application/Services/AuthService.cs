using Application.Dtos;
using Application.IServices;
using Application.Mapper;
using Domain.Entity;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;

    public AuthService(IAuthRepository repo)
    {
        _authRepository = repo;
    }

    public async Task<(bool Success, AuthResponseDto? Response, string Message)> LoginAsync(LoginDto loginDto)
    {
        throw new NotImplementedException();
    }

    public async Task<(bool Success, AuthResponseDto? Response, string Message)> RefreshTokenAsync(TokenDto tokenDto)
    {
        throw new NotImplementedException();
    }

    public async Task<(bool Success, string Message)> LogoutAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task CreateUserAsync(RegisterDto registerDto)
    {
        var applicationUser = registerDto.MapToRegisterDto();
        var userExists = await _authRepository.FindUserByEmailAsync(registerDto.Email);
        if (userExists != null)
        {
            throw new Exception("User is already Registered");
        }

         await _authRepository.CreateUserAsync(applicationUser, registerDto.Password);
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var userList =await _authRepository.GetAllUsersAsync();
        var userListDto = userList.MapToApplicationUsersDto();

        return userListDto;
    }
     
    
    
}