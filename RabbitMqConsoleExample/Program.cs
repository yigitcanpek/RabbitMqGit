// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://vqylgepv:XtIAuRVyAZzSpzFEN2ThnZzZtTfv9M1R@cow.rmq2.cloudamqp.com/vqylgepv");
using (IConnection connection = factory.CreateConnection())
{
    IModel channel = connection.CreateModel();

    channel.ExchangeDeclare("logs-fanout", durable: true/*exhangce saved*/, type: ExchangeType.Fanout);

    /*channel.QueueDeclare("hello-queue", true,false, false);*/ //default way


    Enumerable.Range(1, 50).ToList().ForEach(x => {
        string message = $"log{x}";
        byte[] messageBody = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish("log-fanout",/*, "hello-queue"*/   "", null, messageBody);
        Console.WriteLine($"{message} mesaj gönderilmiştir");
    });

    
     Console.ReadLine();


};

