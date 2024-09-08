using Identity.Domain.Primitives;

namespace Identity.Domain.Entities;

public class ApplicationUserRole : Entity
{
    private ApplicationUserRole()
    {
    }

    private ApplicationUserRole(Guid id, Guid applicationUserId, Guid userRoleId) : base(id)
    {
        Id = id;
        ApplicationUserId = applicationUserId;
        UserRoleId = userRoleId;
    }

    public new Guid Id { get; private set; }

    public ApplicationUser? ApplicationUser { get; private set; }
    public Guid ApplicationUserId { get; private set; }

    public UserRole? UserRole { get; private set; }
    public Guid UserRoleId { get; private set; }

    public static ApplicationUserRole Create(Guid applicationUserId, Guid userRoleId)
    {
        return new ApplicationUserRole(Guid.NewGuid(), applicationUserId, userRoleId);
    }
}