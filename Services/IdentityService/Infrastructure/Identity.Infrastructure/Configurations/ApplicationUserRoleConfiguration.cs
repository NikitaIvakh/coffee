using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Configurations;

public class ApplicationUserRoleConfiguration: IEntityTypeConfiguration<ApplicationUserRole>
{
    public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
    {
        builder.HasKey(key => key.Id);
        builder.HasOne(key => key.ApplicationUser).WithOne().HasForeignKey<ApplicationUserRole>(key => key.ApplicationUserId);
        builder.HasOne(key => key.UserRole).WithOne().HasForeignKey<ApplicationUserRole>(key => key.UserRoleId);
    }
}