using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using TruckDataApplication.Models;

namespace TruckDataApplication
{
    public class Program
    {
        public static void Main()
        {
            var stringMessage = "";
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    stringMessage = message;

                    if (stringMessage == "TruckInfo")
                    {
                        PrintTruckInfo();
                    }
                };
                channel.BasicConsume(queue: "hello",
                                     autoAck: true,
                                     consumer: consumer);

                Console.ReadLine();
            }
        }

        public static void PrintTruckInfo()
        {
            var newTruck = new TruckModel()
            {
                Name = "Truck 61",
                Milage = 47000,
                Height = 2,
                Weight = 1200,
                Latitude = 56.9,
                Longitude = 11.1
            };
            Console.WriteLine("Example Truck");
            Console.WriteLine("_____________________________");
            Console.WriteLine("Name: " + newTruck.Name);
            Console.WriteLine($"Milage: {newTruck.Milage}miles");
            Console.WriteLine($"Height:  {newTruck.Height}m");
            Console.WriteLine($"Weight: {newTruck.Weight}kg");
            Console.WriteLine($"Longitude: {newTruck.Longitude}");
            Console.WriteLine($"Latitude: {newTruck.Latitude}\n");
        }
    }
}
