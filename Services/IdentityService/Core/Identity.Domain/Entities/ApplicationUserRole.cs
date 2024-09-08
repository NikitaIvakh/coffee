using Identity.Domain.Primitives;
using Identity.Domain.Shared;

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

    public static ResultT<ApplicationUserRole> Create(Guid applicationUserId, Guid userRoleId)
    {
        var applicationUserRole = new ApplicationUserRole(Guid.NewGuid(), applicationUserId, userRoleId);
        return Result.Create(applicationUserRole);
    }
}