using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/photo", async (HttpContext context) => 
    {
        await context.Response.WriteAsync("Get photo");
    });
});

public class Photo
{
    public int Id { get; set; }
    public string Name { get; set; }
    public byte[] Data { get; set; }
}   

public class AppDbContext : DbContext
{
    public DbSet<Photo> Photos { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}


