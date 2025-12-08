using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.IServices;
using Domain.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration) => _configuration = configuration;

    public string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        return Convert.ToBase64String(randomBytes);
    }

    public string GenerateAccessToken(ApplicationUser user)
    {
        var claim = GenerateClaims(user);
        var signinCredentials = GetSigninCredentials();
        var token = CreateToken(claim, signinCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private IEnumerable<Claim> GenerateClaims(ApplicationUser user)
    {
        var authClaims = new List<Claim>
        {
            new Claim("id", user.Id),
            new Claim("firstName", user.Firstname!),
            new Claim("lastName", user.Lastname!),
            new Claim("email", user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        return authClaims;
    }

    private SigningCredentials GetSigninCredentials()
    {
        var signinCredentials = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        return new SigningCredentials(signinCredentials, SecurityAlgorithms.HmacSha256);
    }

    private JwtSecurityToken CreateToken(IEnumerable<Claim> claims, SigningCredentials signingCredentials)
    {
        var expirationMinutes = int.Parse(_configuration["Jwt:ExpirationMinutes"]!);
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
            signingCredentials: signingCredentials
        );
        return token;
    }
}