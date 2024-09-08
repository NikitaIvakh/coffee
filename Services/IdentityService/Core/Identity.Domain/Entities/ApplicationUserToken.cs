using Identity.Domain.Primitives;

namespace Identity.Domain.Entities;

public class ApplicationUserToken : Entity
{
    private ApplicationUserToken()
    {
    }

    private ApplicationUserToken(Guid id, Guid applicationUserId, string refreshToken) : base(id)
    {
        Id = id;
        ApplicationUserId = applicationUserId;
        RefreshToken = refreshToken;
    }

    public new Guid Id { get; private set; }

    public ApplicationUser? ApplicationUser { get; private set; }

    public Guid ApplicationUserId { get; private set; }

    public string RefreshToken { get; private set; }

    public static ApplicationUserToken Create(Guid applicationUserId, string refreshToken)
    {
        return new ApplicationUserToken(Guid.NewGuid(), applicationUserId, refreshToken);
    }
}