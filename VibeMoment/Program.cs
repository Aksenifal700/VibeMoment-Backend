using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}


app.UseHttpsRedirection();

app.MapControllers();

app.Run();




























public class Photo
{
    public int Id { get; set; }
    public string Name { get; set; }
    //public byte[] Data { get; set; }
}   

public class AppDbContext : DbContext
{
    public DbSet<Photo> Photos { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}


