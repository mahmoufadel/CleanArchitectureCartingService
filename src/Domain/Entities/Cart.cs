using System.Diagnostics;
using System.Net;
using static System.Net.Mime.MediaTypeNames;
using Image = CleanArchitecture.Domain.ValueObjects.Image;

namespace CleanArchitecture.Domain.Entities;

public class Cart : BaseEventEntity
{
    public Cart()
    {
        Items = new List<CartItem>();
    }
    public Guid Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }

    public string? CreatedBy { get; set; }

    public List<CartItem> Items { get; set; }
}
public class CartItem 
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Image Image { get; set; }
    public Money Price { get; set; }
    public int Quantity { get; set; }
}
/*
 Functional Requirements:

Single entity – Cart
Cart has a unique id which is maintained (generated) on the client-side.
The following actions should be supported:
    Get list of items of the cart object.
    Add item to cart.
    Remove item from the cart.
Each item contains the following info:
    Id – required, id of the item in external system (see Task 2), int.
    Name – required, plain text.
    Image – optional, URL and alt text.
    Price – required, money.
    Quantity – quantity of items in the cart.
 */
