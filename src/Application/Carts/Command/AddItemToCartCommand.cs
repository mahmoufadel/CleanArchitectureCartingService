using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Carts.Commands;

public record AddItemToCartCommand : IRequest<Guid>
{
    public Guid ListId { get; init; }
    public CartItem Item { get; init; }
}

public class AddItemToCartCommandHandler : IRequestHandler<AddItemToCartCommand, Guid>
{
    private readonly IApplicationNoSQLDbContext _context;

    public AddItemToCartCommandHandler(IApplicationNoSQLDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
    {
        var entity = _context.Carts.FindById(request.ListId);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Cart), request.ListId);
        }

        entity.Updated = DateTime.Now;
        entity.Items.Add(request.Item);
      
        _context.Carts.Update(entity);

        return request.Item.Id;
    }

    
}
