using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


namespace mediator.Hare;

public class MyRabbitClient : IRabbitClient
{
    public void Send(string message)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "hello",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: string.Empty,
            routingKey: "hello",
            basicProperties: null,
            body: body);
        Console.WriteLine($" [x] Sent {message}");

        Console.WriteLine(" Press [enter] to exit.");
        //Console.ReadLine();
    }

    public string Receive(string queueName)
    {
        var message = "";
        var factory = new ConnectionFactory { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "hello",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        Console.WriteLine(" [*] Waiting for messages.");

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            message = Encoding.UTF8.GetString(body);
            Console.WriteLine($" [x] Received {message}");
        };
        channel.BasicConsume(queue: queueName,
            autoAck: true,
            consumer: consumer);

        Console.WriteLine(" Press [enter] to exit.");
        // Console.ReadLine();
        return message;
    }
}