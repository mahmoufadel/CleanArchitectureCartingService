using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace RabittmqConsumer;

public static class RabittmqConsumerInit
{
   
    public static void Init(string queueName,string ExchangeName, IConfiguration configuration) {
        var factory = new ConnectionFactory
        {
            UserName = configuration["rabbitmq:username"],

            Password = configuration["rabbitmq:password"],
            HostName = configuration["rabbitmq:hostname"],
            DispatchConsumersAsync = true,
        };
        var connection = factory.CreateConnection();

        var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Fanout, durable: true, autoDelete: false);
        channel.QueueDeclare(
            queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.Received += async (o, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine("Message Get " + message );
            await Task.Yield();
        };

        

        channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

    }
    
}
