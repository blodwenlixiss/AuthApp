using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Auth.Configurations;

public static class JwtAuthConfiguration
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            
        }).AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
                ValidAudience = configuration["Jwt:Audience"],
                ValidIssuer = configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
            };
        });

        services.AddControllers().AddJsonOptions(opt =>
        {
            opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        
        services.AddSwaggerGen(c =>
        {
            c.UseInlineDefinitionsForEnums();
            

            c.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "My API",
                Version = "v1",
                Description = "A simple example of Swagger API documentation."
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "BearerAuth" }
                    },
                    []
                }
            });
        });

        

        return services;
    }
}