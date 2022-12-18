// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Security.Cryptography.X509Certificates;
using System.Text;

Console.WriteLine("Loglar dinleniyor");
ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://vqylgepv:XtIAuRVyAZzSpzFEN2ThnZzZtTfv9M1R@cow.rmq2.cloudamqp.com/vqylgepv");
using (var connection = factory.CreateConnection())
{
    IModel channel = connection.CreateModel();
    //channel.QueueDeclare("hello-queue", true, false, false);

    string randomQueueName = /*"log-database-save-queue";*/ channel.QueueDeclare().QueueName;
    //channel.QueueDeclare(randomQueueName, true, false, false);
    channel.QueueBind(randomQueueName, "logs-fanout", "", null);
    channel.BasicQos(0, 1, false);
    EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

    

    channel.BasicConsume(randomQueueName,false,consumer);  //the second argument is about the queue. If is it "false" subscriber cannot delete message until the another message coming.

    Console.WriteLine("Loglar dinleniyor...");
    consumer.Received += (object? sender, BasicDeliverEventArgs e) =>
    {
        string message = Encoding.UTF8.GetString(e.Body.ToArray());
        Thread.Sleep(1000);
        Console.WriteLine($"gelen log:{message}");
        channel.BasicAck(e.DeliveryTag, true);
    };
    Console.ReadLine();


}
