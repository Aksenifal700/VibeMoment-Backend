using System.Reflection;
using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;
using VibeMoment.Api.Middlewares;
using VibeMoment.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwtConfiguration(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(o =>                                                                                                                                                                                                                                                                                                
           o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));    
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
 
builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly()); 
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

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