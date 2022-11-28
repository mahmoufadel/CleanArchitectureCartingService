using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Application.Role.Command;
using CleanArchitecture.Application.Carts.Commands;

using CleanArchitecture.Application.Carts.Queries.GetCarts;
using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.WebUI.Controllers;

public class RolesController : ApiControllerBase
{
    
    // POST: UsersController/Create
    [HttpPost]  
    public async Task<ActionResult<bool>> Create(CreateRoleCommand createRole)
    {
        return await Mediator.Send(createRole);
    }

    
    [HttpPost("{roleid}/users/{userid}")]
    public async Task<ActionResult<bool>> AddUserToRole([FromRoute] string roleid, [FromRoute] string userid)
    {      
        return await Mediator.Send(new AddUserToRoleCommand(roleid, userid));


    }
}
