using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VibeMoment.Infrastructure.Database.Entities;

namespace VibeMoment.Infrastructure.Database.EntityConfiguration;

public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{

    public void Configure(EntityTypeBuilder<UserProfile> entity)
    {
        entity.HasKey(a => a.Id);
        
        entity.Property(a => a.Id)
            .HasDefaultValueSql("gen_random_uuid()");

        entity.Property(a => a.FirstName)
            .IsRequired()
            .HasMaxLength(50);
        
        entity.Property(a => a.LastName)
            .IsRequired()
            .HasMaxLength(50);
        
        entity.Property(a => a.Bio)
            .IsRequired(false)
            .HasMaxLength(400);
        
        entity.Property(a => a.Avatar)
            .IsRequired(false)
            .HasColumnType("bytea");
        
        entity.Property(a => a.Country)
            .IsRequired(false)
            .HasMaxLength(25);
        
        entity.Property(a =>a.City)
            .IsRequired(false)
            .HasMaxLength(40);
        
        entity.Property(a => a.Gender)
            .IsRequired(false)
            .HasMaxLength(25);

        entity.HasOne(a => a.User)
            .WithOne(a => a.Profile)
            .HasForeignKey<UserProfile>(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired()
            .HasConstraintName("FK_UserProfile_Users");
        
        entity.ToTable("UserProfiles");
    }
}