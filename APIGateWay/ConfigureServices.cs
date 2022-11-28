
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddGateWayServices(this IServiceCollection services, IConfiguration Configuration)
    {
        

        var identityUrl = Configuration.GetValue<string>("IdentityUrl");
       

        var authenticationProviderKey = "OcRequestId";
        Action<JwtBearerOptions> options = x =>
        {
            x.Authority = identityUrl;
            x.RequireHttpsMetadata = false;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false
            };
        };
        services.AddAuthentication().AddJwtBearer(authenticationProviderKey, options);
        
        services.AddOcelot();
        return services;
    }
}
