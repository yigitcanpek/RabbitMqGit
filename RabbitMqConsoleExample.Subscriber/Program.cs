// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Security.Cryptography.X509Certificates;
using System.Text;


ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://vqylgepv:XtIAuRVyAZzSpzFEN2ThnZzZtTfv9M1R@cow.rmq2.cloudamqp.com/vqylgepv");
using IConnection connection = factory.CreateConnection();

    IModel channel = connection.CreateModel();
    channel.ExchangeDeclare("header-exchange", durable: true/*exhangce saved*/, type: ExchangeType.Headers);

    channel.BasicQos(0, 1, false);
    EventingBasicConsumer consumer = new EventingBasicConsumer(channel);



    string queueName = channel.QueueDeclare().QueueName;
    Dictionary<string,object> headers = new Dictionary<string, object>();
    headers.Add("format", "pdf");
    headers.Add("shape", "a4");
    headers.Add("x-match", "all" /* "any" */);
    channel.QueueBind(queueName, "header-exchange",string.Empty,headers);
    channel.BasicConsume(queueName, false, consumer);

    Console.WriteLine("Loglar dinleniyor...");
    consumer.Received += (object? sender, BasicDeliverEventArgs e) =>
    {
        string message = Encoding.UTF8.GetString(e.Body.ToArray());
        Thread.Sleep(1000);
        Console.WriteLine($"gelen log:{message}");
       
        channel.BasicAck(e.DeliveryTag, true);
    };
    Console.ReadLine();



