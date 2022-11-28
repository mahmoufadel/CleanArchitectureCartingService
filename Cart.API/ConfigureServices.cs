using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.WebUI.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Catalog.API.Services;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services, IConfiguration Configuration)
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
               options.Audience = "cart";
               //options.MapInboundClaims=true;             
              

           });
       
        return services;
    }
}
