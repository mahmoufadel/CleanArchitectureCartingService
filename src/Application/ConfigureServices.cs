using System.Reflection;
using AutoMapper;
using CleanArchitecture.Application.Carts.EventHandlers;
using CleanArchitecture.Application.Common.Behaviours;
using CleanArchitecture.Application.RabbitMQ;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Events;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

        services.AddSingleton<IRabbitMqService, RabbitMqService>(); 

        services.AddSingleton<IConnectionFactory>(sp => new ConnectionFactory
        {
            UserName = configuration["rabbitmq:username"],

            Password = configuration["rabbitmq:password"],
            HostName = configuration["rabbitmq:hostname"],
            DispatchConsumersAsync = false,
        });

       
       
        return services;
    }
}
