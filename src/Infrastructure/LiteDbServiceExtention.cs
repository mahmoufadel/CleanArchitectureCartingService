
namespace CleanArchitecture.Infrastructure.Persistence;

using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


public static class LiteDbServiceExtention
{
    public static void AddLiteDb(this IServiceCollection services, string databasePath)
    {
        services.AddTransient<LiteDbContext, LiteDbContext>();
        services.Configure<LiteDbConfig>(options => options.DatabasePath = databasePath);
    }
}
