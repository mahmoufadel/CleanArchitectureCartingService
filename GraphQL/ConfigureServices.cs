using CleanArchitecture.Application.Common.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Catalog.API.Services;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddGraphQLServices(this IServiceCollection services, IConfiguration Configuration)
    {
       
        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        var identityUrl = Configuration.GetValue<string>("IdentityUrl");
        services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
        {
            options.Authority = identityUrl;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                RoleClaimType = "role",
            };
            options.Audience = "graphql";
        });


        services.AddInMemorySubscriptions();
        return services;
    }
}
