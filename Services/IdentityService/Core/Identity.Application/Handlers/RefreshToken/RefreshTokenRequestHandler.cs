using System.IdentityModel.Tokens.Jwt;
using Identity.Application.Abstractors.Interfaces;
using Identity.Application.Extensions;
using Identity.Domain.Common;
using Identity.Domain.Entities;
using Identity.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Identity.Application.Handlers.RefreshToken;

public class RefreshTokenRequestHandler(
    UserManager<ApplicationUser> userManager,
    IConfiguration configuration,
    IApplicationUserTokenRepository applicationUserTokenRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<RefreshTokenRequest, ResultT<ObjectResult>>
{
    public async Task<ResultT<ObjectResult>> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var accessToken = request.TokenModelDto.AccessToken;
            var refreshToken = request.TokenModelDto.RefreshToken;
            var principal = configuration.ClaimsPrincipalFromExpiredToken(accessToken);

            if (principal is null)
                return Result.Failure<ObjectResult>(DomainErrors.ApplicationUserToken.InvalidToken(nameof(accessToken)));

            var userName = principal.Identity!.Name;
            var user = await userManager.FindByNameAsync(userName!);
            var userToken = await applicationUserTokenRepository.GetAllUserTokens().FirstOrDefaultAsync(key => key.UserId == user!.Id, cancellationToken);

            if (user is null || userToken!.RefreshToken != refreshToken || userToken.RefreshTokenExpiryDate <= DateTime.UtcNow)
                return Result.Failure<ObjectResult>(DomainErrors.ApplicationUserToken.InvalidToken(nameof(refreshToken)));

            var newAccessToken = configuration.CreateJwtSecurityToken(principal.Claims.ToList());
            var newRefreshToken = configuration.GenerateRefreshToken();
            ApplicationUserToken.Update(userToken, newRefreshToken);

            var result = new ObjectResult(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                refreshToken = newRefreshToken,
            });

            await applicationUserTokenRepository.UpdateAsync(userToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Result.Create(result);
        }

        catch (Exception exception)
        {
            return Result.Failure<ObjectResult>(DomainErrors.ErrorException.Errors(exception.Message));
        }
    }
}