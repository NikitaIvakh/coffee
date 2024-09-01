using Coffee.Domain.Entities;
using Coffee.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coffee.Infrastructure.Configurations;

public class CoffeeTypeConfiguration: IEntityTypeConfiguration<CoffeeEntity>
{
    public void Configure(EntityTypeBuilder<CoffeeEntity> builder)
    {
        builder.HasKey(key => key.Id);
        builder.Property(key => key.Price).HasColumnType("numeric(10, 2)");
        builder.Property(key => key.CoffeeType).HasConversion<string>();

        builder.HasMany(key => key.CoffeePhotos).WithOne().IsRequired();
    }
}