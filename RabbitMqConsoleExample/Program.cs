// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMqConsoleExample.Publisher;
using System.Text;



ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://vqylgepv:XtIAuRVyAZzSpzFEN2ThnZzZtTfv9M1R@cow.rmq2.cloudamqp.com/vqylgepv");
using IConnection connection = factory.CreateConnection();

    IModel channel = connection.CreateModel();

    channel.ExchangeDeclare("logs-topic", durable: true/*exhangce saved*/, type: ExchangeType.Topic);

    /*channel.QueueDeclare("hello-queue", true,false, false);*/ //default way

   

    Enumerable.Range(1, 50).ToList().ForEach(x => {
        
        Random rnd = new Random();
        LogNames log1 = (LogNames)rnd.Next(1, 5);
        LogNames log2 = (LogNames)rnd.Next(1, 5);
        LogNames log3 = (LogNames)rnd.Next(1, 5);

        string routeKey = $"{log1}.{log2}.{log3}";

        string message = $"log-type:{log1}-{log2}-{log3}";
        byte[] messageBody = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish("logs-topic", routeKey, null, messageBody);
        Console.WriteLine($"{message} Log gönderilmiştir");
    });

    
     Console.ReadLine();


