using MediatR;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TruckApplication.Messages;
using TruckApplication.Models;

namespace TruckApplication.Queries
{
    public class GetTruckInfoQuery : IRequest<TruckModel>
    {
        public TruckMessage Message { get; set; }
    }
    public class GetTruckInfoQueryHandler : IRequestHandler<GetTruckInfoQuery, TruckModel>
    {
        public GetTruckInfoQueryHandler()
        {
        }

        public async Task<TruckModel> Handle(GetTruckInfoQuery request, CancellationToken cancellationToken)
        {
            // Send RabbitMQ Message
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

                string message = "TruckInfo";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }
            return new TruckModel();
        }
    }
}
