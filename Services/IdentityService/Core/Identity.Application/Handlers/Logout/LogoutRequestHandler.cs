using Identity.Application.Abstractors.Interfaces;
using Identity.Domain.Common;
using Identity.Domain.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Handlers.Logout;

public class LogoutRequestHandler(
    IUserRepository userRepository,
    ITokenRepository tokenRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<LogoutRequest, ResultT<Unit>>
{
    public async Task<ResultT<Unit>> Handle(LogoutRequest request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetAllUsers()
            .FirstOrDefaultAsync(key => key.Id == request.Id, cancellationToken);

        if (user is null)
            return Result.Failure<Unit>(DomainErrors.ApplicationUser.UserNotFound($"{request.Id}"));

        var userByToken = await tokenRepository.GetAll()
            .FirstOrDefaultAsync(key => key.ApplicationUserId == user.Id, cancellationToken);

        if (userByToken is null)
            return Result.Failure<Unit>(DomainErrors.ApplicationUser.UserNotFound($"{request.Id}"));

        try
        {
            await userRepository.LogoutAsync(user.Id);
            await tokenRepository.Delete(userByToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        catch (Exception exception)
        {
            return Result.Failure<Unit>(DomainErrors.ErrorException.Errors($"{exception.Message}"));
        }

        return Result.Create(Unit.Value);
    }
}