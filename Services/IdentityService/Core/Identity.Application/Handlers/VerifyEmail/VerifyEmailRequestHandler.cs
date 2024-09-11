using Identity.Application.Abstractors.Interfaces;
using Identity.Domain.Common;
using Identity.Domain.Entities;
using Identity.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Handlers.VerifyEmail;

public class VerifyEmailRequestHandler(
    UserManager<ApplicationUser> userManager,
    IEmailVerificationTokenRepository emailVerificationTokenRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<VerifyEmailRequest, ResultT<bool>>
{
    public async Task<ResultT<bool>> Handle(VerifyEmailRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var token = await emailVerificationTokenRepository
                .GetAll()
                .AsNoTracking()
                .Include(key => key.User)
                .FirstOrDefaultAsync(key => key.Id == request.Id, cancellationToken);

            if (token is null || token.ExpiresAt < DateTime.UtcNow || token.User!.EmailConfirmed)
            {
                return Result.Create(false);
            }

            var user = await userManager.FindByIdAsync(token.User.Id);
            if (user == null)
            {
                return Result.Failure<bool>(DomainErrors.ErrorException.Errors("User not found."));
            }

            user.EmailConfirmed = true;
            await userManager.UpdateAsync(user);
            await emailVerificationTokenRepository.Remove(token);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Create(true);
        }

        catch (Exception e)
        {
            return Result.Failure<bool>(DomainErrors.ErrorException.Errors(e.Message));
        }
    }
}