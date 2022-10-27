using System.Reflection;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Identity;
using CleanArchitecture.Infrastructure.Persistence.Interceptors;
using Duende.IdentityServer.EntityFramework.Options;
using LiteDB;
using  CleanArchitecture.Infrastructure;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Infrastructure.Persistence;

public class LiteDbContext : IApplicationNoSQLDbContext
{
    public readonly LiteDatabase Context;
    public LiteDbContext(IOptions<LiteDbConfig> configs)
    {
        try
        {
            var db = new LiteDatabase(configs.Value.DatabasePath);
            if (db != null)
                Context = db;
        }
        catch (Exception ex)
        {
            throw new Exception("Can find or create LiteDb database.", ex);
        }
    }   
    ILiteCollection<Cart> IApplicationNoSQLDbContext.Carts => Context.GetCollection<Cart>("carts");

    
}