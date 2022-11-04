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

namespace CleanArchitecture.Application.UnitTests.Carts.Commands;


public class CreateCartTests
{
    private Mock<ILogger<CreateCartCommand>> _logger = null!;
    private Mock<IApplicationNoSQLDbContext> _db = null!;
    

    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<CreateCartCommand>>();
        _db = new Mock<IApplicationNoSQLDbContext> ();
        
    }

    [Test]
    [InlineAutoData]
    public async Task ShouldAddCart_Success(CreateCartCommand command)
    {
        var Id = new LiteDB.BsonValue(command.Id);
        _db.Setup(x => x.Carts.Insert(It.IsAny<Cart>())).Returns(Id);

        var handler = new CreateCartCommandHandler(_db.Object);

        var result=await handler.Handle(command, new CancellationToken());

        result.Should().Be(Id.AsGuid);
    }

    [Test]
    [InlineAutoData]
    public async Task ShouldAddCart_Fail(CreateCartCommand command)
    {
        var Id = new LiteDB.BsonValue(command.Id);
        _db.Setup(x => x.Carts.Insert(It.IsAny<Cart>())).Throws<Exception>();

        var handler = new CreateCartCommandHandler(_db.Object);

        await handler.Awaiting( h=> h.Handle(command, new CancellationToken())).Should().ThrowAsync<Exception>();
        
    }

}
