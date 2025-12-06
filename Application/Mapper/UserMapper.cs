using Application.Dtos;
using Domain.Entity;

namespace Application.Mapper;

public static class UserMapper
{
    public static ApplicationUser MapToRegisterDto(this RegisterDto registerDto)
    {
        var applicationUser = new ApplicationUser
        {
            Firstname = registerDto.Firstname,
            Lastname = registerDto.Lastname,
            Email = registerDto.Email
        };

        return applicationUser;
    }

    public static IEnumerable<UserDto> MapToApplicationUsersDto(this IEnumerable<ApplicationUser> users)
    {
        return users.Select(x => new UserDto
        {
            Id = x.Id,
            FirstName = x.Firstname,
            LastName = x.Lastname,
            Username = x.Email,
            Email = x.Email,
            EmailConfirmed = x.EmailConfirmed
        });
    }
}