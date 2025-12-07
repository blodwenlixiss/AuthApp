using System.Security.Claims;
using Domain.Entity;

namespace Application.IServices;

public interface IJwtService
{
 string GenerateAccessToken(ApplicationUser user);
}