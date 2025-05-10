using Microsoft.EntityFrameworkCore;
using VibeMoment.Database.Entities;

namespace VibeMoment.Database;

public class AppDbContext : DbContext
{
    public DbSet<Photo> Photos { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}