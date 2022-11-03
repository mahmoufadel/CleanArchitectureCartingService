using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.ValueObjects;
using MediatR;
namespace CleanArchitecture.Application.Categories.Commands;

public record CreateCategoryCommand : IRequest<Guid>
{
    public Guid Id  { get; init; }
    public string Name { get; set; }

    public string Image { get; set; }
}

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = new Category();
        entity.Created = DateTime.Now;
        entity.Name = request.Name;
        entity.Id = request.Id;
        entity.Image = request.Image;

        await _context.Categories.AddAsync(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
