using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Carts.Commands;

public record RemoveItemFromCartCommand : IRequest<Guid>
{
    public Guid Id  { get; init; }
    public Guid ListId { get; init; }
}

public class RemoveItemFromCartCommandHandler : IRequestHandler<RemoveItemFromCartCommand, Guid>
{
    private readonly IApplicationNoSQLDbContext _context;

    public RemoveItemFromCartCommandHandler(IApplicationNoSQLDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(RemoveItemFromCartCommand request, CancellationToken cancellationToken)
    {
        var entity = _context.Carts.FindById(request.ListId);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Cart), request.ListId);
        }
        entity.Updated = DateTime.Now;
        entity.Items.Remove(entity.Items.First(item=> item.Id==request.Id));

        _context.Carts.Update(entity);
        return entity.Id;

     
    }
}
