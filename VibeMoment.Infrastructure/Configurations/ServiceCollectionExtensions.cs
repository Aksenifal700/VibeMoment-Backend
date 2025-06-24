using Microsoft.Extensions.DependencyInjection;
using VibeMoment.BusinessLogic.Interfaces.Repositories;
using VibeMoment.BusinessLogic.Interfaces.Services;
using VibeMoment.BusinessLogic.Services;
using VibeMoment.Infrastructure.Database.Repositories;

namespace VibeMoment.Infrastructure.Configurations;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPhotoService, PhotoService>();
    }
    public static void AddRepositories(this IServiceCollection services)
    {   
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IPhotoRepository, PhotoRepository>();
    }
}