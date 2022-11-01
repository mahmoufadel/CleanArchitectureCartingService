﻿using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;
namespace CleanArchitecture.Application.Products.Queries;

public class ProductBriefDto : IMapFrom<Product>
{
    public int Id { get; set; }

    public int ListId { get; set; }

    public string? Title { get; set; }

    public bool Done { get; set; }
}
