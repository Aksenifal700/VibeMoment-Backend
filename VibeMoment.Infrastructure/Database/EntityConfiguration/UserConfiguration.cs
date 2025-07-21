using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VibeMoment.Infrastructure.Database.Entities;

namespace VibeMoment.Infrastructure.Database.EntityConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.HasKey(u => u.Id);

        entity.Property(u => u.Id)
            .HasDefaultValueSql("gen_random_uuid()");

        entity.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(50);

        entity.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(50);

        entity.Property(u => u.PasswordHash)
            .IsRequired()
            .HasColumnType("bytea");

        entity.Property(u => u.PasswordSalt)
            .IsRequired()
            .HasColumnType("bytea");

        entity.HasIndex(u => u.Email)
            .IsUnique();

        entity.HasIndex(u => u.UserName)
            .IsUnique();

        entity.ToTable("Users");
    }
}