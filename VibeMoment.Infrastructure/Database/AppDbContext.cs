using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VibeMoment.Infrastructure.Database.Entities;

namespace VibeMoment.Infrastructure.Database
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            ConfigurePhoto(modelBuilder);
            ConfigureRelationships(modelBuilder);
        }

        private void ConfigurePhoto(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Photo>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Id)
                    .ValueGeneratedOnAdd()
                    .UseIdentityColumn();

                entity.Property(p => p.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.Data)
                    .IsRequired()
                    .HasColumnType("bytea");

                entity.Property(p => p.AddedAt)
                    .IsRequired()
                    .HasColumnType("timestamp with time zone")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(p => p.UpdatedAt)
                    .IsRequired(false)
                    .HasColumnType("timestamp with time zone");

                entity.Property(p => p.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasIndex(p => p.UserId)
                    .HasDatabaseName("IX_Photos_UserId");

                entity.HasIndex(p => p.AddedAt)
                    .HasDatabaseName("IX_Photos_AddedAt");

                entity.HasIndex(p => p.Title)
                    .HasDatabaseName("IX_Photos_Title");

                entity.ToTable("Photos");
            });
        }

        private void ConfigureRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Photo>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Photos_AspNetUsers_UserId");
        }
    }
}