using System.Xml;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Security;
using CleanArchitecture.Application.TodoLists.Queries.GetTodos;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using LiteDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Carts.Queries.GetCarts;

public record GetCartsQuery : IRequest<CartsVm>;

public class GetCartsQueryHandler : IRequestHandler<GetCartsQuery, CartsVm>
{
    private readonly IApplicationNoSQLDbContext _context;
    private readonly IMapper _mapper;
    
    public GetCartsQueryHandler(IApplicationNoSQLDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CartsVm> Handle(GetCartsQuery request, CancellationToken cancellationToken)
    {
       // _context.Carts.DeleteAll();
        var items = _context.Carts.Query().ToList();
        return new CartsVm
        {
            Lists = _mapper.Map<List<CartDto>>(items)
        };
    }
}
