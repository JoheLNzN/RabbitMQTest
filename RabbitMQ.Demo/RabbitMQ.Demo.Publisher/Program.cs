
using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new() { 
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

string message = "Funciona mi comunicación entre Pubisher => Consumer";
var body = Encoding.UTF8.GetBytes(message);

channel.BasicPublish(
    exchange: "", 
    routingKey: "queuedemo", 
    basicProperties: null, 
    body
);

Console.WriteLine($"[x] Send: {message}");
Console.ReadLine();