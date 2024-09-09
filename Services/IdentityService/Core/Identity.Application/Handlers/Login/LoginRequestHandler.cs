using Identity.Application.Abstractors.Interfaces;
using Identity.Application.Extensions;
using Identity.Application.Service;
using Identity.Domain.Common;
using Identity.Domain.Entities;
using Identity.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Identity.Application.Handlers.Login;

public sealed class LoginRequestHandler(
    UserManager<ApplicationUser> userManager,
    IApplicationUserTokenRepository applicationUserTokenRepository,
    IConfiguration configuration,
    ITokenService tokenService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<LoginRequest, ResultT<LoginResponse>>
{
    public async Task<ResultT<LoginResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userManager.FindByEmailAsync(request.LoginRequestDto.EmailAddress);

            if (user is null)
                return Result.Failure<LoginResponse>(DomainErrors.ApplicationUser.UserNotFound($"{request.LoginRequestDto.EmailAddress}"));
            
            var checkPasswordUser = await userManager.CheckPasswordAsync(user, request.LoginRequestDto.Password);

            if (!checkPasswordUser)
                return Result.Failure<LoginResponse>(DomainErrors.ApplicationUser.InvalidPassword($"{request.LoginRequestDto.Password}"));
            
            var roles = await userManager.GetRolesAsync(user);
            
            var accessToken = tokenService.CreateToken(user, roles);
            var refreshToken = configuration.GenerateRefreshToken();
            var refreshTokenExpireTime = DateTime.UtcNow.AddDays(int.Parse(configuration.GetSection("Jwt:RefreshTokenValidityInDays").Value!));

            var userToken = ApplicationUserToken.Create(user.Id, refreshToken, refreshTokenExpireTime);

            if (userToken.IsFailure)
                return Result.Failure<LoginResponse>(DomainErrors.ApplicationUserToken.InvalidToken($"{userToken.Error.Message}: {userToken.Error.Message}"));

            var loginResponse = new LoginResponse
            (
                user.FirstName, 
                user.LastName, 
                user.UserName!, 
                user.Email!, 
                accessToken,
                refreshToken
            );
            
            await userManager.UpdateAsync(user);
            await applicationUserTokenRepository.CreateAsync(userToken.Value);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Create(loginResponse);
        }
        
        catch (Exception exception)
        {
           return Result.Failure<LoginResponse>(DomainErrors.ErrorException.Errors(exception.Message));
        }
    }
}