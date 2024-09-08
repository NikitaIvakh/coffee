using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Abstractors.DbContext;

public interface IApplicationDbContext
{
    DbSet<ApplicationUser> ApplicationUsers { get; set; }
    DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
    DbSet<UserRole> UserRoles { get; set; }
    Task<int> SaveChangesAsync(CancellationToken token);
}