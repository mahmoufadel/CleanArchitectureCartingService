using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CleanArchitecture.Application.RabbitMQ;

public interface IRabbitMqService
{
    void Publish(string QueueName, string ExchangeName, object _event);
   
}

public class RabbitMqService : IRabbitMqService
{
    IConnectionFactory _connectionFactory;
    public RabbitMqService(IConnectionFactory connectionFactory)
    {

        _connectionFactory = connectionFactory;
    }
    public void Publish(string QueueName, string ExchangeName, Object _event)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Fanout, durable: true, autoDelete: false);
        channel.QueueDeclare(QueueName, false, false, false, null);
        var message = JsonConvert.SerializeObject(_event);
        var body = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish(ExchangeName, QueueName, null, body);
    }

    

    
}
