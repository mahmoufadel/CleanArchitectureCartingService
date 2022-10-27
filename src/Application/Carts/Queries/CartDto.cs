using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;


namespace CleanArchitecture.Application.Carts.Queries.GetCarts;

public class CartDto : IMapFrom<Cart>
{
    public CartDto()
    {
        Items = new List<CartItemDto>();
    }

    public Guid Id { get; set; }
    public DateTime Created { get; set; }
    public IList<CartItemDto> Items { get; set; }
}
