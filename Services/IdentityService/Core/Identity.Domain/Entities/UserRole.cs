using Identity.Domain.Enums;
using Identity.Domain.Primitives;

namespace Identity.Domain.Entities;

public class UserRole : Entity
{
    private UserRole()
    {
    }

    private UserRole(Guid id, Role role) : base(id)
    {
        Role = role;
    }

    public Role Role { get; private set; }

    public static UserRole Create(Role role)
    {
        return new UserRole(Guid.NewGuid(), role);
    }
}