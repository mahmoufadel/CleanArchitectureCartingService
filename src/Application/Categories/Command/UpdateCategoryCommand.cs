using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Categories.Commands; 
public record UpdateCategoryCommand:IRequest<Guid>
{
    public Guid Id { get; init; }
    public string Name { get; set; }

    public string Image { get; set; }
}
public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public UpdateCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Categories
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Category), request.Id);
        }

        
        entity.Updated = DateTime.Now;
        entity.Name = request.Name;        
        entity.Image = request.Image;

         _context.Categories.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

}
