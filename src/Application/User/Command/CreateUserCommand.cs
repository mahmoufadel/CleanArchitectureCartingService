using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events;
using MediatR;

namespace CleanArchitecture.Application.User.Command;
public record CreateUserCommand : IRequest<(Result Result, string UserId)>
{
    public string  UserId { get; init; }
    public string Password { get; init; }
    
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, (Result Result, string UserId)>
{
    private readonly IIdentityService _identityService;

    public CreateUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<(Result Result, string UserId)> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
      return await _identityService.CreateUserAsync(request.UserId,request.Password);
    }
}