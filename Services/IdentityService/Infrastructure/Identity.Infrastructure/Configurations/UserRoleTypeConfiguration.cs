using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Configurations;

public class UserRoleTypeConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        SeedData(builder);
    }

    private static void SeedData(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                UserId = "9e224968-33e4-4652-b7b7-8574d048cdb9",
                RoleId = "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
            },
            new IdentityUserRole<string>
            {
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                RoleId = "cbc43a8e-f7bb-4445-baaf-1add431ffbbf",
            }
        );
    }
}