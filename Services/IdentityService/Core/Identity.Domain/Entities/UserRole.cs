using Identity.Domain.Enums;
using Identity.Domain.Primitives;

namespace Identity.Domain.Entities;

public class UserRole : Entity
{
    private UserRole()
    {
    }

    private UserRole(Guid id, Guid applicationUserRoleId, Role role) : base(id)
    {
        Id = id;
        ApplicationUserRoleId = applicationUserRoleId;
        Role = role;
    }

    public new Guid Id { get; private set; }
    
    public ApplicationUserRole? ApplicationUserRole { get; private set; }

    public Guid ApplicationUserRoleId { get; private set; }

    public Role Role { get; private set; }

    public static UserRole Create(Guid applicationUserRoleId, Role role)
    {
        return new UserRole(Guid.NewGuid(), applicationUserRoleId, role);
    }
}