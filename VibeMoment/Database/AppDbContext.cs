using Microsoft.EntityFrameworkCore;
using VibeMoment.Database.Entities;

namespace VibeMoment.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<Photo> Photos { get; set; }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Photo>().HasData();
    }

}