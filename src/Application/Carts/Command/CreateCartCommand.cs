using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Carts.Commands;

public record CreateCartCommand : IRequest<Guid>
{
    public Guid Id  { get; init; }
    public List<CartItem> items { get; init; }
}

public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, Guid>
{
    private readonly IApplicationNoSQLDbContext _context;

    public CreateCartCommandHandler(IApplicationNoSQLDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        var entity = new Cart();
        entity.Created = DateTime.Now;

        request.items.ForEach(item => { entity.Items.Add(item); });
        entity.Id = request.Id;

        _context.Carts.Insert(entity);

        return entity.Id;
    }
}
