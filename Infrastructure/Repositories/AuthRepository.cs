using Domain.Entity;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly AppDbContext _context;


    public AuthRepository(AppDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
    {
        var userList = await _context.ApplicationUsers.ToListAsync();

        return userList;
    }
}