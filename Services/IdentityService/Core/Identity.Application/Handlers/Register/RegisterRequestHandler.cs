using System.Security.Claims;
using FluentEmail.Core;
using Identity.Application.Abstractors.Interfaces;
using Identity.Application.Helpers;
using Identity.Domain.Common;
using Identity.Domain.Entities;
using Identity.Domain.Enums;
using Identity.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;

namespace Identity.Application.Handlers.Register;

public sealed class RegisterRequestHandler(
    UserManager<ApplicationUser> userManager, 
    IEmailVerificationTokenRepository emailVerificationTokenRepository,
    IFluentEmail fluentEmail,
    EmailVerificationLinkFactory emailVerificationLinkFactory,
    IHttpContextAccessor httpContextAccessor,
    LinkGenerator linkGenerator,
    IUnitOfWork unitOfWork)
    : IRequestHandler<RegisterRequest, ResultT<RegisterResponse>>
{
    public async Task<ResultT<RegisterResponse>> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var userExistsByUserName = await userManager.FindByNameAsync(request.RegisterRequestDto.UserName);
            if (userExistsByUserName != null)
                return Result.Failure<RegisterResponse>(DomainErrors.ApplicationUser.AlreadyExists($"{request.RegisterRequestDto.UserName}"));

            var userExistsByEmail = await userManager.FindByEmailAsync(request.RegisterRequestDto.EmailAddress);
            if (userExistsByEmail != null)
                return Result.Failure<RegisterResponse>(DomainErrors.ApplicationUser.AlreadyExists($"{request.RegisterRequestDto.EmailAddress}"));

            if (request.RegisterRequestDto.Password != request.RegisterRequestDto.PasswordConform)
                return Result.Failure<RegisterResponse>(DomainErrors.ApplicationUser.PasswordsIsNotConfirmed($"{nameof(request.RegisterRequestDto.UserName)}"));

            var correctUserResult = ApplicationUser.Create(
                id: Guid.NewGuid().ToString(),
                firstName: request.RegisterRequestDto.FirstName,
                lastName: request.RegisterRequestDto.LastName,
                userName: request.RegisterRequestDto.UserName,
                email: request.RegisterRequestDto.EmailAddress,
                passwordHash: new PasswordHasher<ApplicationUser>().HashPassword(null, request.RegisterRequestDto.Password),
                securityStamp: Guid.NewGuid().ToString(),
                emailConfirmed: false
            );

            if (correctUserResult.IsFailure)
                return Result.Failure<RegisterResponse>(DomainErrors.ApplicationUser.UserCanNotRegister($"{correctUserResult.Error.Code}: {correctUserResult.Error.Message}"));

            var correctUser = correctUserResult.Value;
            var result = await userManager.CreateAsync(correctUser, request.RegisterRequestDto.Password);

            if (!result.Succeeded)
                return Result.Failure<RegisterResponse>(DomainErrors.ApplicationUser.UserCanNotRegister($"{correctUserResult.Error.Code}: {correctUserResult.Error.Message}"));
            
            await userManager.AddToRoleAsync(correctUser, nameof(Role.User)); 

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, correctUser.UserName!),
                new(ClaimTypes.Email, correctUser.Email!),
            };
            
            var verificationToken = EmailVerificationToken.Create
            (
                userId: correctUser.Id,
                createdAt: DateTime.UtcNow,
                expiresAt: DateTime.UtcNow.AddDays(1)
            );
            
            if (verificationToken.IsFailure)
                return Result.Failure<RegisterResponse>(verificationToken.Error);
            
            await userManager.AddClaimsAsync(correctUser, claims);
            await userManager.UpdateAsync(correctUser);
            await emailVerificationTokenRepository.Create(verificationToken.Value);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var registerResponse = new RegisterResponse(correctUser.Id);

            var verificationLink = new EmailVerificationLinkFactory(httpContextAccessor, linkGenerator);
            var verificationLinkCreate = verificationLink.Create(verificationToken.Value);

            await fluentEmail
                .To(correctUser.Email)
                .Subject("Email verification for CalConnect")
                .Body($"To verify your email address, <a href=\"{verificationLinkCreate}\">click here</a>", isHtml: true)
                .SendAsync();
            
            return Result.Create(registerResponse);
        }

        catch (Exception exception)
        {
            return Result.Failure<RegisterResponse>(DomainErrors.ErrorException.Errors(exception.Message));
        }
    }
}