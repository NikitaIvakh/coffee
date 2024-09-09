using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Configurations;

public class ApplicationUserTokenConfiguration: IEntityTypeConfiguration<ApplicationUserToken>
{
    public void Configure(EntityTypeBuilder<ApplicationUserToken> builder)
    {
        builder.HasKey(key => key.Id);
        builder.Ignore(key => key.User);
        
        builder.HasOne(key => key.User).WithOne().HasForeignKey<ApplicationUserToken>(key => key.UserId);
    }
}