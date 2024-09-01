using Coffee.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coffee.Infrastructure.Configurations;

public class CoffeePhotoConfiguration : IEntityTypeConfiguration<CoffeePhoto>
{
    public void Configure(EntityTypeBuilder<CoffeePhoto> builder)
    {
        builder.HasKey(key => key.Id);
    }
}