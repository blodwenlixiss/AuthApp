using Application.Dtos;
using Application.IServices;
using Domain.Entity;
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

    [HttpGet("allUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        var userList = await _authService.GetAllUsersAsync();
        return Ok(userList);
    }
}