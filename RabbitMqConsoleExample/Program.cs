// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMqConsoleExample.Publisher;
using System.Text;



ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://vqylgepv:XtIAuRVyAZzSpzFEN2ThnZzZtTfv9M1R@cow.rmq2.cloudamqp.com/vqylgepv");
using IConnection connection = factory.CreateConnection();

    IModel channel = connection.CreateModel();

    channel.ExchangeDeclare("logs-direct", durable: true/*exhangce saved*/, type: ExchangeType.Direct);

    /*channel.QueueDeclare("hello-queue", true,false, false);*/ //default way

    Enum.GetNames(typeof(LogNames)).ToList().ForEach(x=>
    {
        string routeKey = $"route-{x}";
        string queueName = $"direct-queue -{x}";
        channel.QueueDeclare(queueName, true, false, false);
        channel.QueueBind(queueName,"logs-direct",routeKey,null);
    });

    Enumerable.Range(1, 50).ToList().ForEach(x => {
        LogNames log = (LogNames)new Random().Next(1, 5);
        string message = $"log-type:{log}";
        byte[] messageBody = Encoding.UTF8.GetBytes(message);
        string routeKey = $"route-{log}";
        

        channel.BasicPublish("logs-direct", routeKey, null, messageBody);
        Console.WriteLine($"{message} Log gönderilmiştir");
    });

    
     Console.ReadLine();


