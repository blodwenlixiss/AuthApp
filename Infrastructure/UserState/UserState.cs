using System.Security.Claims;
using Application.Dtos;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.UserState;

public class UserState : IUserState
{
    private readonly IHttpContextAccessor _httpContextAccessorcontextAccessor;

    public UserState(IHttpContextAccessor accessor)
    {
        _httpContextAccessorcontextAccessor = accessor;
    }

    public CurrentUser GetCurrentUser()
    {
        var user = _httpContextAccessorcontextAccessor.HttpContext?.User ??
                   throw new NotFoundException("User not found!");
        var userId = user.FindFirst(c => c.Type == "id");
        var userEmail = user.FindFirst(c => c.Type == ClaimTypes.Email);

        if (userId == null || userEmail == null)
            throw new UnauthorizedAccessException("No Authorize to this user");

        return new CurrentUser(userId.Value, userEmail.Value);
    }
}