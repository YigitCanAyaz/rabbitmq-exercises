using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory();

factory.Uri = new Uri("amqps://wxrhdmxb:Z4BKct0LOg1frdiJYsxjdhxp05K2vHMk@woodpecker.rmq.cloudamqp.com/wxrhdmxb");

using var connection = factory.CreateConnection();

var channel = connection.CreateModel();

// optional to use, if we sure that this queue exist in the publisher side we don't need this code, this code will generate queue if doesn't exist. we know that this queue exist in the publisher side we don't this code really. (parameters should be same too!)
// channel.QueueDeclare("hello-queue", true, false, false);

// true => x = (6 / process)
// false => x = process = 6
// below code means send 1 message to every subscriber
channel.BasicQos(0, 1, false);

// consumer means subscriber
var consumer = new EventingBasicConsumer(channel);

// autoAck => false (it means, i will let you when you will remove message from queue)
channel.BasicConsume("hello-queue", false, consumer);

consumer.Received += (object? sender, BasicDeliverEventArgs e) =>
{
    var message = Encoding.UTF8.GetString(e.Body.ToArray());

    Thread.Sleep(1500);

    Console.WriteLine("The message: " + message);

    // giving news to publisher (you can delete this message from queue)
    channel.BasicAck(e.DeliveryTag, false);
};

Console.ReadLine();