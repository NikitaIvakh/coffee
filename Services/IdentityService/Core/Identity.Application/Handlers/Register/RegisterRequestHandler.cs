using Identity.Application.Abstractors.Interfaces;
using Identity.Application.Helpers;
using Identity.Domain.Common;
using Identity.Domain.Entities;
using Identity.Domain.Enums;
using Identity.Domain.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Handlers.Register;

public sealed class RegisterRequestHandler(
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<RegisterRequest, ResultT<Guid>>
{
    public async Task<ResultT<Guid>> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        var dto = request.RegisterRequestDto;

        var existingUser = await userRepository.GetAllUsers()
            .FirstOrDefaultAsync(u => u.UserName == dto.UserName || u.EmailAddress == dto.EmailAddress, cancellationToken);

        if (existingUser != null)
            return Result.Failure<Guid>(DomainErrors.ApplicationUser.AlreadyExists($"{(existingUser.UserName == dto.UserName ? dto.UserName : dto.EmailAddress)}"));

        if (dto.Password != dto.PasswordConform)
            return Result.Failure<Guid>(DomainErrors.ApplicationUser.PasswordsIsNotConfirmed("Passwords do not match"));

        HashPasswordHelper.HashPassword(dto.Password, out var salt);

        var user = ApplicationUser.Create(
            dto.FirstName,
            dto.LastName,
            dto.UserName,
            dto.EmailAddress,
            Convert.ToHexString(salt),
            Convert.ToHexString(salt)
        );

        if (user.IsFailure)
            return Result.Failure<Guid>(DomainErrors.ApplicationUser.UserCanNotRegister($"{user.Error.Code}: {user.Error.Message}"));

        var userRole = UserRole.Create(Role.User);
        var applicationUserRole = ApplicationUserRole.Create(user.Value.Id, userRole.Value.Id);

        try
        {
            await userRepository.RegisterAsync(user.Value);
            await roleRepository.Create(applicationUserRole.Value);
            await roleRepository.Create(userRole.Value);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
        
        catch (Exception ex)
        {
            return Result.Failure<Guid>(DomainErrors.ErrorException.Errors($"Registration failed: {ex.Message}"));
        }

        return Result.Create(user.Value.Id);
    }
}