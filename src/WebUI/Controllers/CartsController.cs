using CleanArchitecture.Application.Carts.Commands;

using CleanArchitecture.Application.Carts.Queries.GetCarts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers;


public class CartsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<CartsVm>> Get()
    {
        return await Mediator.Send(new GetCartsQuery());
    }

    [HttpPost]
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

}
