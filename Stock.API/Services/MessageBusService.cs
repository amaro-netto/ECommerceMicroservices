using RabbitMQ.Client;
using System.Text;

namespace Stock.API.Services
{
    public class MessageBusService : IMessageBusService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private const string QUEUE_NAME = "sales-orders";

        public MessageBusService()
        {
            var factory = new ConnectionFactory
            {
                HostName = "192.168.0.110",
                UserName = "user_vendas",
                Password = "senha_forte_aqui"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: QUEUE_NAME,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        public void PublishMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "",
                                  routingKey: QUEUE_NAME,
                                  basicProperties: null,
                                  body: body);
        }
    }
}