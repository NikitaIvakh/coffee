using System.Security.Claims;
using Identity.Application.Abstractors.Interfaces;
using Identity.Domain.Common;
using Identity.Domain.Entities;
using Identity.Domain.Enums;
using Identity.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Handlers.Register;

public sealed class RegisterRequestHandler(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
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
                securityStamp: Guid.NewGuid().ToString()
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
            
            await userManager.AddClaimsAsync(correctUser, claims);
            await userManager.UpdateAsync(correctUser);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var registerResponse = new RegisterResponse(correctUser.Id);
            return Result.Create(registerResponse);
        }

        catch (Exception exception)
        {
            return Result.Failure<RegisterResponse>(DomainErrors.ErrorException.Errors(exception.Message));
        }
    }
}