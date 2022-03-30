using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RevendaVeiculos.Rabbit.Models;
using System.Text;

namespace RevendaVeiculos.Rabbit.Producers
{
    public class NotificacaoEmailProducer
    {
        private readonly ConnectionFactory _factory;
        private readonly RabbitMqConfiguration _config;
        public NotificacaoEmailProducer(IOptions<RabbitMqConfiguration> options)
        {
            _config = options.Value;

            _factory = new ConnectionFactory
            {
                HostName = _config.Host
            };
        }

        public bool PostNotificacao(EmailInputModel message)
        {
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: _config.Queue,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    var stringfiedMessage = JsonConvert.SerializeObject(message);
                    var bytesMessage = Encoding.UTF8.GetBytes(stringfiedMessage);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: _config.Queue,
                        basicProperties: null,
                        body: bytesMessage);
                }
            }

            return true;
        }
    }
}
