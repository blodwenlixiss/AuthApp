using System.Security.Claims;
using Application.Dtos;
using Application.IServices;
using Application.Mapper;
using Azure.Core;
using Domain.Entity;
using Domain.Exceptions;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly UserManager<ApplicationUser> _manager;
    private readonly IJwtService _jwtService;
    private readonly AppDbContext _context;
    private readonly IUserState _userState;


    public AuthService(
        IUserState userState,
        AppDbContext context,
        UserManager<ApplicationUser> manager,
        IAuthRepository repo,
        IJwtService jwtService
    )
    {
        _userState = userState;
        _context = context;
        _authRepository = repo;
        _manager = manager;
        _jwtService = jwtService;
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _manager.FindByEmailAsync(loginDto.Email) ??
                   throw new NotFoundException("User not found");

        var accessToken = _jwtService.GenerateAccessToken(user);
        var refreshToken = _jwtService.GenerateRefreshToken();

        await _authRepository.CreateRefreshTokenAsync(MapToRefreshToken(user.Id, refreshToken));

        await _context.SaveChangesAsync();

        var response = new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };

        return response;
    }

    public async Task Logout()
    {
        var user = _userState.GetCurrentUser();
        var refreshToken = await _authRepository.GetRefreshTokenFromUserId(user.Id);

        if (refreshToken is null)
            throw new NotFoundException("Refreshtoken is not found");

        foreach (var refresh in refreshToken)
        {
            refresh.IsRevoked = true;
        }

        await _context.SaveChangesAsync();
    }

    private RefreshToken MapToRefreshToken(string userId, string token)
    {
        return new RefreshToken()
        {
            UserId = userId,
            Token = token,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
        };
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