using CleanArchitecture.Application.Carts.Commands;

using CleanArchitecture.Application.Carts.Queries.GetCarts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class CartsController : ApiControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CartDto>> GetItem(Guid id)
    {
        return await Mediator.Send(new GetCartQuery(id));
    }

    [HttpGet]   
    public async Task<ActionResult<CartsVm>> GetAll()
    {
        return await Mediator.Send(new GetCartsQuery());
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult<Guid>> Create(CreateCartCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await Mediator.Send(new DeleteCartCommand(id));

        return NoContent();
    }

    [HttpDelete ("{cartId}/items/{id}")]    
    public async Task<ActionResult> DeleteItem([FromRoute] Guid cartId, [FromRoute] Guid id)
    {
        await Mediator.Send(new RemoveItemFromCartCommand { Id = id, CartId = cartId });

        return NoContent();
    }

    [HttpPost("{cartId}/items/")]
    public async Task<ActionResult<Guid>> AddItem(AddItemToCartCommand addItemToCartCommand)
    {
        return await Mediator.Send(addItemToCartCommand);

        
    }
}
