using Identity.Domain.Shared;

namespace Identity.Domain.Common;

public static class DomainErrors
{
    public static class ErrorException
    {
        public static readonly Func<string, Error> Errors = (property) =>
            new Error("exception.error", $"{property}");
    }
    
    public static class ApplicationUser
    {
        public static readonly Func<string, Error> AlreadyExists = (userName) =>
            new Error("user.already.exists", $"{userName} is already exists");

        public static readonly Func<string, Error> UserCanNotRegister = (error) =>
            new Error("user.ca.not.register", $"{error}");

        public static readonly Func<string, Error> PasswordsIsNotConfirmed = (property) =>
            new Error("passwords.is.not.confirmed", "Passwords is not confirmed");

        public static readonly Func<string, Error> UserNotFound = (property) =>
            new Error("user.not.found", $"user with email {property} not found");
    }
}