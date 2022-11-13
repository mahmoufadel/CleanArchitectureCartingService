using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.RabbitMQ;
using CleanArchitecture.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace CleanArchitecture.Application.Carts.EventHandlers;
public class CartCreatedEventHandler : INotificationHandler<CartCreatedEvent>
{
    private readonly ILogger<CartCreatedEventHandler> _logger;
    IRabbitMqService _rabbitMqService;

    public CartCreatedEventHandler(ILogger<CartCreatedEventHandler> logger, IRabbitMqService rabbitMqService)
    {
        _logger = logger;
        _rabbitMqService = rabbitMqService;
    }

    public Task Handle(CartCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        _rabbitMqService.Publish("Cart", "Carts", notification._cart);

        return Task.CompletedTask;
    }
}
