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
    public async Task CreateRefreshTokenAsync(RefreshToken refreshToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken);
    }

    public async Task<IEnumerable<RefreshToken>?> GetRefreshTokenFromUserId(string userId)
    {
        var refrehsToken = _context.RefreshTokens
            .Include(rf => rf.User)
            .Where(r => r.IsRevoked == false && r.UserId == userId);

        return await Task.FromResult(refrehsToken);
    }

    public async Task<RefreshToken?> GetRefreshTokenAsync(string token)
    {
        var refreshToken = await _context.RefreshTokens
            .Include(rf => rf.User)
            .FirstOrDefaultAsync(r => r.Token == token);
        return refreshToken;
    }

    public async Task InvalidateRefreshToken(string token)
    {
        var refreshToken = await GetRefreshTokenAsync(token);
        if (refreshToken != null)
        {
            refreshToken.IsRevoked = true;
        }
    }
}