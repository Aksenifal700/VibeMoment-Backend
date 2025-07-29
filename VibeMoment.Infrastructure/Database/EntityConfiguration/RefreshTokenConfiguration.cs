using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VibeMoment.Infrastructure.Database.Entities;

namespace VibeMoment.Infrastructure.Database.EntityConfiguration;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> entity)
    {
        entity.HasKey(r => r.Id);

        entity.Property(r => r.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .ValueGeneratedOnAdd();

        entity.Property(r => r.Token)
            .IsRequired()
            .HasMaxLength(200);

        entity.Property(rt => rt.ExpiresOnUtc)
            .IsRequired();
        
        entity.Property(r => r.IsRevoked)
            .IsRequired()
            .HasDefaultValue(false);

        entity.HasOne(r => r.User)
            .WithMany()
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_RefreshTokens_Users_UserId");

        entity.ToTable("RefreshTokens");
    }
    
}