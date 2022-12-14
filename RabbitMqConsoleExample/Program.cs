// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://vqylgepv:XtIAuRVyAZzSpzFEN2ThnZzZtTfv9M1R@cow.rmq2.cloudamqp.com/vqylgepv");
using (var connection = factory.CreateConnection())
{
    IModel channel = connection.CreateModel();
    channel.QueueDeclare("hello-queue", true,false, false);
    string message = "hello world";
    byte[] messageBody = Encoding.UTF8.GetBytes(message);
    channel.BasicPublish(string.Empty,"hello-queue",null,messageBody);
    Console.WriteLine($"{message} mesaj gönderilmiştir");
    Console.ReadLine();


};

