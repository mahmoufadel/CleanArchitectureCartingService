using System.Xml;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Categories.Queries;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Security;
using CleanArchitecture.Application.TodoLists.Queries.GetTodos;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using LiteDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Categories.Queries;


public record GetCategoriesQuery: IRequest<CategoriesVm>;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, CategoriesVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    
    public GetCategoriesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CategoriesVm> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {        
        var entities = _context.Categories.ToList();
        return new CategoriesVm { Lists = _mapper.Map<List<CategoryDto>>(entities) };
        
    }
}
