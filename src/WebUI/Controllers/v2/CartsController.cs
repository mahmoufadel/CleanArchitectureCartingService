using CleanArchitecture.Application.Carts.Commands;

using CleanArchitecture.Application.Carts.Queries.GetCarts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers.Carts.V2;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("2.0")]
public class CartsController : ApiControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<List<CartItemDto>>> GetItem(Guid id)
    {
        return await Mediator.Send(new GetCartItemsQuery(id));
    }

   
    
}
