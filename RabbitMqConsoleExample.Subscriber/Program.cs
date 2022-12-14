﻿// See https://aka.ms/new-console-template for more information
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
    EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
    channel.BasicConsume("hello-queue",false,consumer);
    consumer.Received += (object? sender, BasicDeliverEventArgs e) =>
    {
        string message = Encoding.UTF8.GetString(e.Body.ToArray());
        Console.WriteLine($"gelen mesaj:{message}");
    };
    Console.ReadLine();


}