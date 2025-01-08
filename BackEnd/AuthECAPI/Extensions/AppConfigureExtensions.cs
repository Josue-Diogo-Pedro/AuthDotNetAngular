using AuthECAPI.Models;

namespace AuthECAPI.Extensions;

public static class AppConfigureExtensions
{
    public static WebApplication ConfigureCORS(this WebApplication app)
    {
        app.UseCors(options => 
            options.WithOrigins("http://localhost:4200")
                   .AllowAnyMethod()
                   .AllowAnyHeader());

        return app;
    }

    public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
        return services;
    }
}