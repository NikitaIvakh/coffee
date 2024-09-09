using Identity.Application.Abstractors.Interfaces;
using Identity.Domain.Common;
using Identity.Domain.Entities;
using Identity.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Handlers.RevokeToken;

public class RevokeTokenRequestHandler(
    UserManager<ApplicationUser> userManager,
    IApplicationUserTokenRepository applicationUserTokenRepository,
    IUnitOfWork unitOfWork) 
    : IRequestHandler<RevokeTokenRequest, ResultT<Unit>>
{
    public async Task<ResultT<Unit>> Handle(RevokeTokenRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userManager.FindByIdAsync(request.Id.ToString());

            if (user is null)
                return Result.Failure<Unit>(DomainErrors.ApplicationUser.UserNotFound($"{request.Id}"));
            
            var userToken = await applicationUserTokenRepository
                .GetAllUserTokens()
                .FirstOrDefaultAsync(key => key.UserId == user.Id, cancellationToken);

            if (userToken is null)
                return Result.Failure<Unit>(DomainErrors.ApplicationUserToken.TokenNotFound($"{request.Id}"));

            await applicationUserTokenRepository.DeleteAsync(userToken);
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