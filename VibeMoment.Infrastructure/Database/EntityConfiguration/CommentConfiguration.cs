using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VibeMoment.Infrastructure.Database.Entities;

namespace VibeMoment.Infrastructure.Database.EntityConfiguration;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> entity)
    {
        entity.HasKey(c => c.Id);
        
        entity.Property(c => c.Id)
            .HasDefaultValueSql("gen_random_uuid()");
        
        entity.Property(c => c.Content)
            .IsRequired()
            .HasMaxLength(1488);

        entity.Property(c => c.CreatedAt)
            .IsRequired()
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
            
        entity.ToTable("Comment");
        
        entity.HasOne<Photo>()
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.PhotoId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne<User>()
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}