using CleanArchitecture.Application.Categories.Commands;
using GraphQL.Queries;

namespace GraphQL.Subscriptions;

public class Subscription
{
    [Subscribe]
    public Guid CategoryAdded([EventMessage] CreateCategoryCommand command) 
    {
        Console.WriteLine($"Subscribe working good with Name : {command.Name}");
        return command.Id;
    }
}




