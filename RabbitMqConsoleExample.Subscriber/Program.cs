// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Security.Cryptography.X509Certificates;
using System.Text;

Console.WriteLine("Hello, World!");
ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://vqylgepv:XtIAuRVyAZzSpzFEN2ThnZzZtTfv9M1R@cow.rmq2.cloudamqp.com/vqylgepv");
using (var connection = factory.CreateConnection())
{
    IModel channel = connection.CreateModel();
    //channel.QueueDeclare("hello-queue", true, false, false);
    channel.BasicQos(0, 1, false);
    EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
    channel.BasicConsume("hello-queue",false,consumer); //the second argument is about the queue. If is it "false" subscriber cannot delete message until the another message coming.
    consumer.Received += (object? sender, BasicDeliverEventArgs e) =>
    {
        string message = Encoding.UTF8.GetString(e.Body.ToArray());
        Console.WriteLine($"gelen mesaj:{message}");
        channel.BasicAck(e.DeliveryTag, true);
    };
    Console.ReadLine();


}
