using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Configurations;

public class UserRoleConfiguration: IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(key => key.Id);
        builder.HasOne(key => key.ApplicationUserRole).WithOne(key => key.UserRole).HasForeignKey<UserRole>(key => key.ApplicationUserRoleId);
        builder.Property(key => key.Role).HasConversion<string>();
    }
}