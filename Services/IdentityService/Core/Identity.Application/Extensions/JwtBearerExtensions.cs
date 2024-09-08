using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Identity.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Identity.Application.Extensions;

public static class JwtBearerExtensions
{
    public static IEnumerable<Claim> CreateClaims(this ApplicationUser user, string role)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(DateTime.Now).ToString(CultureInfo.CurrentCulture), ClaimValueTypes.Integer64),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.EmailAddress),
        };

        return claims;
    }

    private static SigningCredentials CreateSigningCredentials(this IConfiguration configuration)
    {
        return new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)),
            SecurityAlgorithms.HmacSha256);
    }

    public static JwtSecurityToken CreateJwtSecurityToken(this IEnumerable<Claim> claims, IConfiguration configuration)
    {
        var expireTime = int.Parse(configuration.GetSection("Jwt:Expire").Value!);

        return new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expireTime),
            signingCredentials: configuration.CreateSigningCredentials()
        );
    }

    public static JwtSecurityToken CreateJwtSecurityToken(this IConfiguration configuration, IEnumerable<Claim> claims)
    {
        var expireTime = int.Parse(configuration.GetSection("Jwt:Expire").Value!);

        return new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expireTime),
            signingCredentials: configuration.CreateSigningCredentials()
        );
    }

    public static string GenerateRefreshToken(this IConfiguration configuration)
    {
        var randomNumber = new byte[64];
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }

    public static ClaimsPrincipal? ClaimsPrincipalFromExpiredToken(this IConfiguration configuration, string? token)
    {
        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!))
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Token is invalid");
        }

        return principal;
    }
}