using Identity.Domain.Common;
using Identity.Domain.Enums;
using Identity.Domain.Primitives;
using Identity.Domain.Shared;

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

    public static ResultT<UserRole> Create(Role role)
    {
        if (string.IsNullOrEmpty(role.ToString()))
            return Result.Failure<UserRole>(DomainErrors.UserRole.InvalidValue(nameof(role)));
        
        var userRole = new UserRole(Guid.NewGuid(), role);
        return Result.Create(userRole);
    }
}