
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using IdentityServer.Persistence;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using IdentityServer;
using Duende.IdentityServer.Services;
using IdentityServer.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddIdentityServerServices(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("CleanArchitectureDb"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }



        services.AddScoped<ApplicationDbContextInitialiser>();

        services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        /*  services.AddAuthentication().AddIdentityServerJwt();

          services.AddAuthorization(options =>  options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));

          */
        var cl = new AspNetCore.ApiAuthorization.IdentityServer.ClientCollection();
        cl.AddRange(Config.Clients.ToArray());
        var scopes = new AspNetCore.ApiAuthorization.IdentityServer.ApiScopeCollection();
        scopes.AddRange(Config.ApiScopes.ToArray());
        var identityResources = new AspNetCore.ApiAuthorization.IdentityServer.IdentityResourceCollection();
        identityResources.AddRange(Config.IdentityResources.ToArray());
        services.AddIdentityServer(options =>
             {
                 options.Events.RaiseErrorEvents = true;
                 options.Events.RaiseInformationEvents = true;
                 options.Events.RaiseFailureEvents = true;
                 options.Events.RaiseSuccessEvents = true;

                 // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                 options.EmitStaticAudienceClaim = true;
             })
             .AddInMemoryIdentityResources(Config.IdentityResources)
             .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddApiAuthorization<ApplicationUser, ApplicationDbContext>( op=> {
                op.Clients = cl;
                op.ApiScopes = scopes;
                op.IdentityResources = identityResources;
            }).AddProfileService<ProfileService>();

       
        return services;
    }
}
