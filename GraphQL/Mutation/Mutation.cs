using CleanArchitecture.Application.Categories.Commands;
using GraphQL.Queries;
using HotChocolate.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GraphQL.Mutation;

public class Mutation
{
    public async Task<ResultVM> Create([Service] IMediator mediator, [Service] ITopicEventSender sender, CreateCategoryCommand command)
    {
        await sender.SendAsync("CategoryAdded", command);
        return new ResultVM { Id = await mediator.Send(command) };               
    }

    public async Task<ResultVM> Update([Service] IMediator mediator,UpdateCategoryCommand command)
    {
        await mediator.Send(command);
        return new ResultVM { Id = command.Id };
    }
}

public class ResultVM {
    public Guid Id { get; set; }
}
