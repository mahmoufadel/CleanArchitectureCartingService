using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.ValueObjects;
using MediatR;

namespace CleanArchitecture.Application.Products.Commands;

public record UpdateProductCommand : IRequest<Guid>
{
    public Guid Id { get; set; }

    public Guid CategoryId { get; set; }

    public string Name { get; set; }

    public string Image { get; set; }
    public Money Price { get; set; }
    public int Amount { get; set; }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand,Guid>
{
    private readonly IApplicationDbContext _context;

    public UpdateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Products
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Product), request.Id);
        }

        entity.Name = request.Name;
        entity.Price = request.Price;
        entity.Amount = request.Amount;
        entity.Image = request.Image;

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
