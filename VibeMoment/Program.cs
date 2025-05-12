using Microsoft.EntityFrameworkCore;
using VibeMoment.MappingProfiles;
using VibeMoment.Services;
using VibeMoment.Services.Interfaces;
using VibeMoment.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(PhotoProfile)); // Укажіть тип класу вашого профілю

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(
    builder.Configuration.GetConnectionString("DefaultConnection"),
    npgsqlOptions => npgsqlOptions.EnableRetryOnFailure(
        maxRetryCount:5,
        maxRetryDelay: TimeSpan.FromSeconds(10),
        errorCodesToAdd:null 
        ) 
    )
);
//builder.Services.AddSingleton<IPhotoService, PhotoService>();
builder.Services.AddScoped<IPhotoService, PhotoService>();  // Dependency injection lifetimes
//builder.Services.AddTransient<IPhotoService, PhotoService>();

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

































