using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory();

// In the real scenario just put this url in appsettings.json
factory.Uri = new Uri("amqps://wxrhdmxb:Z4BKct0LOg1frdiJYsxjdhxp05K2vHMk@woodpecker.rmq.cloudamqp.com/wxrhdmxb");

// end of the main scope it will be discarded
using var connection = factory.CreateConnection();

var channel = connection.CreateModel();

// creatin queue
channel.QueueDeclare("hello-queue", true, false, false);

string message = "hello world";

// converting to byte because rabbitmq only takes byte as an input
var messageBody = Encoding.UTF8.GetBytes(message);

// string.Empty bcs we r not using exchange yet
// hello-queue => routingKey
channel.BasicPublish(string.Empty, "hello-queue", null, messageBody);

Console.WriteLine("Mesaj gönderilmiştir");

Console.ReadLine();