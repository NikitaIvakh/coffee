using Identity.Domain.Primitives;

namespace Identity.Domain.Entities;

public class ApplicationUser : Entity, IAuditableData
{
    private ApplicationUser()
    {
    }

    private ApplicationUser(Guid id, string firstName, string lastName, string userName, string emailAddress,
        string password, string passwordConfirm) : base(id)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        EmailAddress = emailAddress;
        Password = password;
        PasswordConfirm = passwordConfirm;
    }

    public new Guid Id { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string UserName { get; private set; }

    public string EmailAddress { get; private set; }

    public string Password { get; private set; }

    public string PasswordConfirm { get; private set; }

    public DateTimeOffset CreatedAt { get; set; }

    public static ApplicationUser Create(string firstName, string lastName, string userName, string emailAddress,
        string password, string passwordConfirm)
    {
        return new ApplicationUser(Guid.NewGuid(), firstName, lastName, userName, emailAddress, password, passwordConfirm);
    }
}