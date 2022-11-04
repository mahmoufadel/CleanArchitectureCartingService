using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Carts.Commands;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.IntegrationTests.Carts.Commands;

using static Testing;

public class CreateCartTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateCartCommand();
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRequireUniqueTitle()
    {
        await SendAsync(new CreateCartCommand
        {
            Id = Guid.NewGuid()
        });

        var command = new CreateCartCommand
        {
            Id = Guid.NewGuid()
        };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateTodoList()
    {
        var userId = await RunAsDefaultUserAsync();

        var command = new CreateCartCommand
        {
            Id = Guid.NewGuid()
        };

        var id = await SendAsync(command);

        var list = await FindAsync<Cart>(id);

        list.Should().NotBeNull();
        list!.Id.Should().Be(command.Id);
        list.CreatedBy.Should().Be(userId);
        list.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
