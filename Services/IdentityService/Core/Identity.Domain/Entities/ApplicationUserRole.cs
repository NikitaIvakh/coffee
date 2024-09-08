using Identity.Domain.Primitives;

namespace Identity.Domain.Entities;

public class ApplicationUserRole : Entity
{
    private ApplicationUserRole()
    {
    }

    private ApplicationUserRole(Guid id, Guid applicationUserId, Guid userRoleId) : base(id)
    {
        ApplicationUserId = applicationUserId;
        UserRoleId = userRoleId;
    }

    public ApplicationUser? ApplicationUser { get; private set; }
    public Guid ApplicationUserId { get; private set; }

    public UserRole? UserRole { get; private set; }
    public Guid UserRoleId { get; private set; }

    public static ApplicationUserRole Create(Guid applicationUserId, Guid userRoleId)
    {
        return new ApplicationUserRole(Guid.NewGuid(), applicationUserId, userRoleId);
    }
}