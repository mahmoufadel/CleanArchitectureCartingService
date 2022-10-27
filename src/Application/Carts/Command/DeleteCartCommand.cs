using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Carts.Commands;

public record DeleteCartCommand(Guid Id) : IRequest<Guid>;

public class DeleteCartCommandHandler : IRequestHandler<DeleteCartCommand,Guid>
{
    private readonly IApplicationNoSQLDbContext _context;

    public DeleteCartCommandHandler(IApplicationNoSQLDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        _context.Carts.Delete(new LiteDB.BsonValue(request.Id));          
        return request.Id;
    }

    
}
