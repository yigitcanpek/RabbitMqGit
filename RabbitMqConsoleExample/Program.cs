// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMqConsoleExample.Publisher;
using System.Text;



ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://vqylgepv:XtIAuRVyAZzSpzFEN2ThnZzZtTfv9M1R@cow.rmq2.cloudamqp.com/vqylgepv");
using IConnection connection = factory.CreateConnection();

    IModel channel = connection.CreateModel();

    channel.ExchangeDeclare("header-exchange", durable: true/*exhangce saved*/, type: ExchangeType.Headers);
    Dictionary<string,object> headers = new Dictionary<string, object>();
    headers.Add("format", "pdf");
    headers.Add("shape", "a4");
    IBasicProperties properties = channel.CreateBasicProperties();
    properties.Headers = headers;
    properties.Persistent = true;
    channel.BasicPublish("header-exchange", string.Empty, properties,Encoding.UTF8.GetBytes("header mesajım"));

    Console.WriteLine("mesaj gönderilmiştir");


    
     Console.ReadLine();


