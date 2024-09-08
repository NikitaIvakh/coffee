using Identity.Application.Abstractors.Interfaces;
using Identity.Domain.Entities;

namespace Identity.Infrastructure.Repository;

public class RoleRepository(ApplicationDbContext context) : IRoleRepository
{
    public async Task Create(UserRole userRole)
    {
        ArgumentNullException.ThrowIfNull(userRole);
        await context.AddAsync(userRole);
    }

    public async Task Create(ApplicationUserRole applicationUserRole)
    {
        ArgumentNullException.ThrowIfNull(applicationUserRole);
        await context.AddAsync(applicationUserRole);
    }
}