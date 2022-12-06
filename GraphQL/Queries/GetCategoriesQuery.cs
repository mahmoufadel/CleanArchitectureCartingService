
using System.Threading;
using CleanArchitecture.Application.Categories.Queries;
using HotChocolate;
using HotChocolate.Types;
using MediatR;
using System.Data;
using HotChocolate.AspNetCore.Authorization;
using CleanArchitecture.Application.Role;

namespace GraphQL.Queries;


public class Book
{
    public string Title { get; set; }

    public Author Author { get; set; }
}

public class Author
{
    public string Name { get; set; }
}

public class Query
{

    
    [Authorize(Roles = new[] { RolesConst.RoleAdministrator })]
    //[Authorize]
    public async Task<CategoriesVm> GetCategory([Service] IMediator mediator, CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetCategoriesQuery(), cancellationToken);
    }
    public Book GetBook([Service] IMediator mediator, CancellationToken cancellationToken)
    {

        return
            new Book
            {
                Title = "C# in depth.",
                Author = new Author
                {
                    Name = "Jon Skeet"
                }
            };
    }

    public Author GetAuthor(int id)
    {

        return
            new Author
            {                
                    Name = "Jon Skeet"
               
            };
    }
}




