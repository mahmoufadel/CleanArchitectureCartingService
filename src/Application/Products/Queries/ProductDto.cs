using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Application.Products.Queries;


public class ProductDto : IMapFrom<Product>
{
    public Guid Id { get; set; }

    public Guid CategoryId { get; set; }

    public string Name { get; set; }

    public string Image { get; set; }
    public Money Price { get; set; }
    public int Amount { get; set; }


}
