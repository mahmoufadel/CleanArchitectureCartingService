using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Carts.Commands;
using CleanArchitecture.Application.Common.Behaviours;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.TodoItems.Commands.CreateTodoItem;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using CleanArchitecture.Domain.Entities;
using Azure.Core;
using AutoFixture.NUnit3;
using FluentAssertions;
using LiteDB;
using CleanArchitecture.Application.Common.Exceptions;

namespace CleanArchitecture.Application.UnitTests.Carts.Commands;


public class RemoveItemFromCartTests
{
    private Mock<ILogger<RemoveItemFromCartCommand>> _logger = null!;
    private Mock<IApplicationNoSQLDbContext> _db = null!;
    

    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<RemoveItemFromCartCommand>>();
        _db = new Mock<IApplicationNoSQLDbContext> ();
        
    }

    [Test]
    [InlineAutoData]
    public async Task ShouldRemoveItemFromCart_Success(RemoveItemFromCartCommand command,Cart cart, CartItem item)
    {
        item.Id = command.Id;
        cart.Items.Add(item);
        _db.Setup(x => x.Carts.FindById(It.IsAny<BsonValue>())).Returns(cart);
        _db.Setup(x => x.Carts.Update(It.IsAny<Cart>())).Returns(true);

        var handler = new RemoveItemFromCartCommandHandler(_db.Object);

        await handler.Handle(command, new CancellationToken());

        cart.Items.Count(i=>i.Id==item.Id).Should().Be(0);
    }

    [Test]
    [InlineAutoData]
    public async Task ShouldRemoveItemFromCart_Fail(RemoveItemFromCartCommand command)
    {
        
        _db.Setup(x => x.Carts.FindById(It.IsAny<BsonValue>())).Returns( (Cart)null);

        var handler = new RemoveItemFromCartCommandHandler(_db.Object);

        await handler.Awaiting( h=> h.Handle(command, new CancellationToken())).Should().ThrowAsync<NotFoundException>();
        
    }

}
