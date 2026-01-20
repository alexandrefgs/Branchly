using Branchly.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Branchly.Infrastructure.Persistence.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedNever();

        builder.Property(u => u.Username)
            .HasConversion(
                username => username.Value,
                value => new(value))
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(u => u.Username)
            .IsUnique();
    }
}
