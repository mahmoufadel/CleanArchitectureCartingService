using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Events;
public class CartCreatedEvent : BaseEvent
{
    public CartCreatedEvent(Cart cart)
    {
        _cart = cart;
    }
    public Cart _cart { get; }
}