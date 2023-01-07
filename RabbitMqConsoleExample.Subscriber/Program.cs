// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Security.Cryptography.X509Certificates;
using System.Text;


ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://vqylgepv:XtIAuRVyAZzSpzFEN2ThnZzZtTfv9M1R@cow.rmq2.cloudamqp.com/vqylgepv");
using IConnection connection = factory.CreateConnection();

    IModel channel = connection.CreateModel();
    

    channel.BasicQos(0, 1, false);
    EventingBasicConsumer consumer = new EventingBasicConsumer(channel);



    string queueName = channel.QueueDeclare().QueueName;
    string routeKey = "*.Error.*"; /* "Error.#" */
    channel.QueueBind(queueName, "logs-topic",routeKey);
    
    channel.BasicConsume(queueName, false, consumer);

    Console.WriteLine("Loglar dinleniyor...");
    consumer.Received += (object? sender, BasicDeliverEventArgs e) =>
    {
        string message = Encoding.UTF8.GetString(e.Body.ToArray());
        Thread.Sleep(1000);
        Console.WriteLine($"gelen log:{message}");
        //File.AppendAllText("log-critical.txt", message+ "\n");
        channel.BasicAck(e.DeliveryTag, true);
    };
    Console.ReadLine();



