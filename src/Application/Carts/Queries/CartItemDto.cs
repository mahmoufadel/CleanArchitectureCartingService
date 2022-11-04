using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.ValueObjects;
namespace CleanArchitecture.Application.Carts.Queries.GetCarts;

public class CartItemDto : IMapFrom<CartItem>
{
    public Guid Id { get; set; }

    

    public string Name { get; set; }

    public Image Image { get; set; }
    public Money Price { get; set; }
    public int Quantity { get; set; }


}
