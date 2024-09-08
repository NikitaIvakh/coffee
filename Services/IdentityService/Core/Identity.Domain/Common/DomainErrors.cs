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
            new Error("user.can.not.register", $"{error}");

        public static readonly Func<string, Error> PasswordsIsNotConfirmed = (property) =>
            new Error("passwords.is.not.confirmed", "Passwords is not confirmed");

        public static readonly Func<string, Error> UserNotFound = (property) =>
            new Error("user.not.found", $"user with email {property} not found");

        public static readonly Func<string, Error> InvalidLength = (property) =>
            new Error("invalid.length", $"{property}: length is invalid");

        public static readonly Func<string, Error> InvalidEmailAddress = (property) =>
            new Error("email.address.is.invalid", $"{property} is invalid email address");
    }

    public static class ApplicationUserToken
    {
        public static readonly Func<string, Error> InvalidLength = (property) =>
            new Error("invalid.length", $"{property}: length is invalid");
    }

    public static class UserRole
    {
        public static readonly Func<string, Error> InvalidValue = (property) =>
            new Error("value.is.invalid", $"{property} is invalid");
    }
}