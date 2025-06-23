using VibeMoment.Api.Configurations;
using VibeMoment.Api.Middlewares;
using VibeMoment.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddDataBaseContext(builder.Configuration);
builder.Services.AddIdentityServices();
builder.Services.AddAutomapper();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();