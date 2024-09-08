using System.IdentityModel.Tokens.Jwt;
using Identity.Application.Extensions;
using Identity.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Identity.Application.Service;

public class TokenService(IConfiguration configuration) : ITokenService
{
    public string CreateToken(ApplicationUser user)
    {
        var token = user.CreateClaims().CreateJwtSecurityToken(configuration);
        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(token);
    }
}