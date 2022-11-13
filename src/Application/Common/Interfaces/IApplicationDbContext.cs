using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using LiteDB;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<Category> Categories { get; }

    DbSet<Product> Products { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    
}

public interface IApplicationNoSQLDbContext
{
    ILiteCollection<Cart> Carts { get; }
    Task PublishEvent(BaseEvent @event);


}
