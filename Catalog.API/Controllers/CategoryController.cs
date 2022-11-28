
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Application.Categories.Queries;
using CleanArchitecture.Application.Categories.Commands;

namespace CleanArchitecture.WebUI.Controllers;

[Authorize]
public class CategorysController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<CategoriesVm>> Get()
    {
        return await Mediator.Send(new GetCategoriesQuery ());
    }

    [HttpGet("{id}")]
    public async Task<CategoryDto> Get(Guid id)
    {               
        return await Mediator.Send(new GetCategoryQuery(id));
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateCategoryCommand command)
    {
        return await Mediator.Send(command);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateCategoryCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await Mediator.Send(new DeleteCategoryCommand(id));

        return NoContent();
    }

   
}
