using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory();

factory.Uri = new Uri("amqps://wxrhdmxb:Z4BKct0LOg1frdiJYsxjdhxp05K2vHMk@woodpecker.rmq.cloudamqp.com/wxrhdmxb");

using var connection = factory.CreateConnection();

var channel = connection.CreateModel();

// optional to use, if we sure that this queue exist in the publisher side we don't need this code, this code will generate queue if doesn't exist. we know that this queue exist in the publisher side we don't this code really. (parameters should be same too!)
// channel.QueueDeclare("hello-queue", true, false, false);

// consumer means subscriber
var consumer = new EventingBasicConsumer(channel);

// autoAck => false (just send success messages)
channel.BasicConsume("hello-queue", true, consumer);

consumer.Received += (object? sender, BasicDeliverEventArgs e) =>
{
    var message = Encoding.UTF8.GetString(e.Body.ToArray());

    Console.WriteLine("The message: " + message);
};

Console.ReadLine();