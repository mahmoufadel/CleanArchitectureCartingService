using CleanArchitecture.Application.Carts.Commands;

using CleanArchitecture.Application.Carts.Queries.GetCarts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers;


public class CartsController : ApiControllerBase
{
    [HttpGet("GetItem/{id}")]
    public async Task<ActionResult<CartDto>> GetItem(Guid id)
    {
        return await Mediator.Send(new GetCartQuery(id));
    }

    [HttpGet]
    [Route("All")]
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

    [HttpDelete]
    [Route("DeleteItem")]
    public async Task<ActionResult> DeleteItem(RemoveItemFromCartCommand removeItemFromCartCommand)
    {
        await Mediator.Send(removeItemFromCartCommand);

        return NoContent();
    }

    [HttpPost]
    [Route("AddItem")]
    public async Task<ActionResult<Guid>> AddItem(AddItemToCartCommand addItemToCartCommand)
    {
        return await Mediator.Send(addItemToCartCommand);

        
    }
}
