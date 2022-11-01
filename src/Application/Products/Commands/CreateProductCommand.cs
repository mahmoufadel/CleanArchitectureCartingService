using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events;
using CleanArchitecture.Domain.ValueObjects;
using MediatR;

namespace CleanArchitecture.Application.Products.Commands;

public record CreateProductCommand : IRequest<Guid>,IMapFrom<Product>
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public Money Price { get; set; }
    public int Amount { get; set; }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = new Product();
        entity.Id=request.Id;
        entity.CategoryId = request.CategoryId;
        entity.Amount = request.Amount;
        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.Image = request.Image;
        entity.Price = request.Price;

        _context.Products.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
