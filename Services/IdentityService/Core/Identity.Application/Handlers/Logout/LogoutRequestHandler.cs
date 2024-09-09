using Identity.Application.Abstractors.Interfaces;
using Identity.Domain.Common;
using Identity.Domain.Entities;
using Identity.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Handlers.Logout;

public class LogoutRequestHandler(
    UserManager<ApplicationUser> userManager,
    IApplicationUserTokenRepository applicationUserTokenRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<LogoutRequest, ResultT<Unit>>
{
    public async Task<ResultT<Unit>> Handle(LogoutRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userManager.FindByIdAsync(request.Id.ToString());

            if (user is null)
                return Result.Failure<Unit>(DomainErrors.ApplicationUser.UserNotFound($"{request.Id}"));

            var userTokens = await applicationUserTokenRepository
                .GetAllUserTokens()
                .Where(token => token.UserId == user.Id)
                .ToListAsync(cancellationToken);
            
            foreach (var token in userTokens)
            {
                await applicationUserTokenRepository.DeleteAsync(token);
            }

            await userManager.UpdateAsync(user);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Create(Unit.Value);
        }

        catch (Exception exception)
        {
            return Result.Failure<Unit>(DomainErrors.ErrorException.Errors(exception.Message));
        }
    }
}