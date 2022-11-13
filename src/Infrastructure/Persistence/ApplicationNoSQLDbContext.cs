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
using MediatR;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Application.Carts.Queries.GetCarts;
using CleanArchitecture.Domain.Common;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CleanArchitecture.Infrastructure.Persistence;

public class LiteDbContext : IApplicationNoSQLDbContext
{
    public readonly LiteDatabase Context;
    private readonly IPublisher _publisher;
    private readonly ILogger<LiteDbContext> _logger;
    public LiteDbContext(IOptions<LiteDbConfig> configs, IMediator mediator,
        IPublisher publisher,
        ILogger<LiteDbContext> logger)
    {
        try
        {
          
            _publisher = publisher;
            _logger = logger;
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
    

    async Task IApplicationNoSQLDbContext.PublishEvent(BaseEvent @event) =>      await _publisher.Publish(@event);    
    
}