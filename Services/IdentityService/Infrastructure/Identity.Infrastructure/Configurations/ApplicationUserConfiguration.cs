using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Configurations;

public class ApplicationUserConfiguration: IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        SeedData(builder);
    }

    private static void SeedData(EntityTypeBuilder<ApplicationUser> builder)
    {
        var hasher = new PasswordHasher<ApplicationUser>();
        
        var data1 = ApplicationUser.Create(
            id: "9e224968-33e4-4652-b7b7-8574d048cdb9",
            firstName: "System",
            lastName: "User",
            userName: "user@localhost.com",
            email: "user@localhost.com",
            passwordHash: hasher.HashPassword(null, "P@ssword1"),
            securityStamp: Guid.NewGuid().ToString()
        );

        var data2 = ApplicationUser.Create(
            id: "8e445865-a24d-4543-a6c6-9443d048cdb9",
            firstName: "System",
            lastName: "Admin",
            userName: "admin@localhost.com",
            email: "admin@localhost.com",
            passwordHash: hasher.HashPassword(null, "P@ssword1"),
            securityStamp: Guid.NewGuid().ToString()
        );
        
        builder.HasData(data1.Value, data2.Value);
    }
}