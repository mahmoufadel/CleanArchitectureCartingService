using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Application.User.Command;
using CleanArchitecture.Application.Carts.Commands;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Carts.Queries.GetCarts;
using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.WebUI.Controllers;

public class UsersController : ApiControllerBase
{

    // POST: UsersController/Create
    [HttpPost]
    public async Task<ActionResult<(Result Result, string UserId)>> Create(CreateUserCommand userCommand)
    {
        return await Mediator.Send(userCommand);
    }


}
