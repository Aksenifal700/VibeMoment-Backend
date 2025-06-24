using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using VibeMoment.Infrastructure.Database;

namespace VibeMoment.Infrastructure.Configurations;

public static class IdentityConfiguration
{
    public static void AddIdentityServices(this IServiceCollection services)
    {
        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
    }
}