using Identity.Application.Abstractors.Interfaces;
using Identity.Application.Extensions;
using Identity.Application.Service;
using Identity.Domain.Common;
using Identity.Domain.Entities;
using Identity.Domain.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Identity.Application.Handlers.Login;

public class LoginRequestHandler(
    ITokenRepository tokenRepository,
    IUserRepository userRepository,
    ITokenService tokenService,
    IConfiguration configuration,
    IUnitOfWork unitOfWork)
    : IRequestHandler<LoginRequest, ResultT<LoginResponse>>
{
    public async Task<ResultT<LoginResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var dto = request.LoginRequestDto;

        var user = await userRepository
            .GetAllUsers()
            .FirstOrDefaultAsync(key => key.EmailAddress == dto.EmailAddress, cancellationToken);

        if (user is null)
            return Result.Failure<LoginResponse>(DomainErrors.ApplicationUser.UserNotFound($"{dto.EmailAddress}"));

        var accessToken = tokenService.CreateToken(user);
        var refreshToken = configuration.GenerateRefreshToken();
        var refreshTokenExpiresTime = DateTime.UtcNow.AddDays(int.Parse(configuration.GetSection("Jwt:RefreshTokenValidityInDays").Value!));
        var userWithToken = ApplicationUserToken.Create(user.Id, refreshToken, refreshTokenExpiresTime);
        var loginResponse = new LoginResponse(accessToken, refreshToken, user.UserName, user.EmailAddress);

        try
        {
            var userById = await userRepository.GetAllUsers().FirstOrDefaultAsync(key => key.Id == user.Id, cancellationToken);
            
            if (userById is null)
                await tokenRepository.Create(userWithToken.Value);

            else
                await tokenRepository.Update(userWithToken.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        catch (Exception exception)
        {
            return Result.Failure<LoginResponse>(DomainErrors.ErrorException.Errors($"{exception.Message}"));
        }

        return Result.Create(loginResponse);
    }
}