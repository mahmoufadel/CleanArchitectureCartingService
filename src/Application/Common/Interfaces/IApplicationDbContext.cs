﻿using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using LiteDB;
namespace CleanArchitecture.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

   

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

public interface IApplicationNoSQLDbContext
{
    ILiteCollection<Cart> Carts { get; }
     
}
