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


    public AuthRepository(UserManager<ApplicationUser> manager, AppDbContext context)
    {
        _userManager = manager;
        _context = context;
    }

    public async Task CreateUserAsync(ApplicationUser user, string password)
    {
         await _userManager.CreateAsync(user, password);
         await _context.SaveChangesAsync();
    }

    public async Task<ApplicationUser?> FindUserByEmailAsync(string email)
    {
        var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u=>u.Email == email);
        return user;
    }

    public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
    {
        var userList = await _context.ApplicationUsers.ToListAsync();

        return userList;
    }

    public async Task<ApplicationUser?> FindUserByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user;
    }

    public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
    {
        var userPassword = await _userManager.CheckPasswordAsync(user, password);
        return userPassword;
    }


    public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
    {
        var userUpdate = await _userManager.UpdateAsync(user);
        return userUpdate;
    }
}