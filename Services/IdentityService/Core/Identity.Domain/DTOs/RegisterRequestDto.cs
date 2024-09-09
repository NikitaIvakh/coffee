namespace Identity.Domain.DTOs;

public record RegisterRequestDto(
    string FirstName,
    string LastName,
    string UserName,
    string EmailAddress,
    string Password,
    string PasswordConform
);