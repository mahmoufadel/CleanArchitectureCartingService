using AutoMapper;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Carts.Queries.GetCarts;

public record GetCartItemsQuery(Guid Id) : IRequest<List<CartItemDto>>;

public class GetCartItemsQueryHandler : IRequestHandler<GetCartItemsQuery, List<CartItemDto>>
{
    private readonly IApplicationNoSQLDbContext _context;
    private readonly IMapper _mapper;

    public GetCartItemsQueryHandler(IApplicationNoSQLDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CartItemDto>> Handle(GetCartItemsQuery request, CancellationToken cancellationToken)
    {
        var entity = _context.Carts.FindById(request.Id);
        return _mapper.Map<List<CartItemDto>>(entity.Items);


    }
}