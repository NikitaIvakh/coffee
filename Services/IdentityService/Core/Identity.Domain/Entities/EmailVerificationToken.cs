using Identity.Domain.Shared;

namespace Identity.Domain.Entities;

public class EmailVerificationToken
{
    private EmailVerificationToken()
    {
    }

    private EmailVerificationToken(string id, string userId, DateTime createdAt, DateTime expiresAt)
    {
        Id = id;
        UserId = userId;
        CreatedAt = createdAt;
        ExpiresAt = expiresAt;
    }

    public string Id { get; private set; } = string.Empty;

    public string UserId { get; private set; } = string.Empty;

    public DateTime CreatedAt { get; private set; }

    public DateTime ExpiresAt { get; private set; }

    public virtual ApplicationUser? User { get; private set; }

    public static ResultT<EmailVerificationToken> Create(string userId, DateTime createdAt, DateTime expiresAt)
    {
        var emailVerificationToken = new EmailVerificationToken(Guid.NewGuid().ToString(), userId, createdAt, expiresAt);
        return Result.Create(emailVerificationToken);
    }
}