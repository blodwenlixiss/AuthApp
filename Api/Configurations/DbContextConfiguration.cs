using Application.IServices;
using Application.Services;
using Domain.Entity;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auth.Configurations;

public static class DbContextConfiguration
{
    public static IServiceCollection AddDbConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddCors(opt => opt.AddPolicy("All", corsPolicyBuilder =>
        {
            corsPolicyBuilder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
        }));

        services.AddDbContext<AppDbContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("Default")));

        // services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthService,AuthService>();
        services.AddScoped<IAuthRepository,AuthRepository>();

        return services;
    }
}