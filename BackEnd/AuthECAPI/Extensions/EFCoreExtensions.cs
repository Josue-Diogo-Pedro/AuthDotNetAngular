using System.Security.Claims;
using AuthECAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthECAPI.Extensions;

public static class EFCoreExtensions
{
    public static IServiceCollection InjectDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        //services.AddHttpContextAccessor(); -- used to use ClaimsPrincipal in your layers. It's so helpful when we want to kwnow which user is logged

        return services;
    }
}