using System.Security.Claims;
using Application.Dtos;
using Application.IServices;
using Application.Mapper;
using Azure.Core;
using Domain.Entity;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly UserManager<ApplicationUser> _manager;
    private readonly IJwtService _jwtService;
    private readonly IConfiguration _configuration;

    public AuthService(
        UserManager<ApplicationUser> manager,
        IAuthRepository repo,
        IJwtService jwtService,
        IConfiguration configuration
    )
    {
        _authRepository = repo;
        _manager = manager;
        _jwtService = jwtService;
        _configuration = configuration;
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _manager.FindByEmailAsync(loginDto.Email) ??
                   throw new NotFoundException("User not found");

        var accessToken = _jwtService.GenerateAccessToken(user);

        var response = new AuthResponseDto
        {
            UserId = user.Id,
            FirstName = user.Firstname,
            LastName = user.Lastname,
            Email = user.Email!,
            AccessToken = accessToken,
            Expiration = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:ExpirationMinutes"]!))
        };

        return response;
    }


    public async Task CreateUserAsync(RegisterDto registerDto)
    {
        var applicationUser = registerDto.MapToRegisterDto();
        var userExists = await _manager.FindByEmailAsync(registerDto.Email);
        if (userExists != null)
        {
            throw new BadRequestException("User is already Registered");
        }

        await _manager.CreateAsync(applicationUser, registerDto.Password);
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var userList = await _authRepository.GetAllUsersAsync();

        if (userList == null)
        {
            throw new BadRequestException("Something Went Wrong");
        }

        var userListDto = userList.MapToApplicationUsersDto();

        return userListDto;
    }
}