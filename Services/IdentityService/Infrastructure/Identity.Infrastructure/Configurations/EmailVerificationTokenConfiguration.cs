using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Configurations;

public class EmailVerificationTokenConfiguration: IEntityTypeConfiguration<EmailVerificationToken>
{
    public void Configure(EntityTypeBuilder<EmailVerificationToken> builder)
    {
        builder.HasKey(key => key.Id);
        builder.Ignore(key => key.User);

        builder.HasOne(key => key.User).WithOne().HasForeignKey<EmailVerificationToken>(key => key.UserId);
    }
}