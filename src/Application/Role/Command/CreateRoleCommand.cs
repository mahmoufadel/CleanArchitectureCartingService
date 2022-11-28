using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events;
using MediatR;

namespace CleanArchitecture.Application.Role.Command;
public record CreateRoleCommand : IRequest<bool>
{    
    public string Name { get; init; }
    
}

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, bool>
{
    private readonly IIdentityService _identityService;

    public CreateRoleCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<bool> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
      var result= await _identityService.AddRoleAsync(request.Name);

        
        return result;
    }
}

public record AddUserToRoleCommand (string Name, string UserId) : IRequest<bool>
;

public class AddUserToRoleCommandHandler : IRequestHandler<AddUserToRoleCommand, bool>
{
    private readonly IIdentityService _identityService;

    public AddUserToRoleCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<bool> Handle(AddUserToRoleCommand request, CancellationToken cancellationToken)
    {
        var result = await _identityService.AddUserToRoleAsync(request.Name, request.UserId);
        return result;
    }
}