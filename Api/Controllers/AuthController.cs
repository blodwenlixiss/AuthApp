using System.IdentityModel.Tokens.Jwt;
using Application.Dtos;
using Application.IServices;
using Domain.Entity;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;


    public AuthController(IAuthService authService) => _authService = authService;


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        await _authService.CreateUserAsync(registerDto);
        return Ok("User Registered Successfully");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var userLogin = await _authService.LoginAsync(loginDto);
        var result = ResponseModel<object>.SuccessResponse(userLogin);
        return Ok(result);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> LogoOut()
    {
        await _authService.Logout();
        return NoContent();
    }

    [Authorize]
    [HttpGet("allUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        var userList = await _authService.GetAllUsersAsync();
        return Ok(userList);
    }
}