using CleanArchitecture.Application.Carts.Queries.GetCarts;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Products.Queries;
using CleanArchitecture.Domain.Entities;


namespace CleanArchitecture.Application.Categories.Queries;

public class CategoryDto : IMapFrom<Category>
{
    public CategoryDto()
    {
        Products = new List<ProductDto>();
    }

    public Guid Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public IList<ProductDto> Products { get; set; }
}
