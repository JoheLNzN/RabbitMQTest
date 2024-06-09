using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new()
{
    HostName = "localhost",
    UserName = "admin",
    Password = "admin"
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(
    queue: "queuedemo",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null
);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += Consumer_Received;

channel.BasicConsume(
    queue: "queuedemo",
    autoAck: true,
    consumer
);

void Consumer_Received(object? sender, BasicDeliverEventArgs e)
{
    var body = e.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine($"[x] Received: {message}");
}

Console.ReadLine();