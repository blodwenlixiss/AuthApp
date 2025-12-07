using Domain.Entity;
using Microsoft.AspNetCore.Identity;

namespace Domain.Interfaces;

public interface IAuthRepository
{
    Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
}