using AutoMapper;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Carts.Queries.GetCarts;

public record GetCartQuery(Guid Id) : IRequest<CartDto>;

public class GetCartQueryHandler : IRequestHandler<GetCartQuery, CartDto>
{
    private readonly IApplicationNoSQLDbContext _context;
    private readonly IMapper _mapper;

    public GetCartQueryHandler(IApplicationNoSQLDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CartDto> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var entity = _context.Carts.FindById(request.Id);
        return _mapper.Map<CartDto>(entity);


    }
}