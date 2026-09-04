using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniOrderManagement.Domain.Entities;

namespace MiniOrderManagement.Infrastructure.Persistence.Configurations;

public class CustomerProfileConfiguration
    : IEntityTypeConfiguration<CustomerProfile>
{
    public void Configure(
        EntityTypeBuilder<CustomerProfile> builder)
    {
        builder.ToTable("CustomerProfiles");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Address)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(x => x.CustomerId)
            .IsUnique();
    }
}