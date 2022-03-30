using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RevendaVeiculos.Message.Models;
using RevendaVeiculos.Message.Services;
using System.Text;

namespace RevendaVeiculos.Message.Consumers
{
    public class NotificacaoEmailConsumer : BackgroundService
    {
        private readonly RabbitMqConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<NotificacaoEmailConsumer> _logger;


        public NotificacaoEmailConsumer(ILogger<NotificacaoEmailConsumer> logger, IOptions<RabbitMqConfiguration> option, IServiceProvider serviceProvider)
        {
            _logger = logger;

            _configuration = option.Value;
            _serviceProvider = serviceProvider;

            var factory = new ConnectionFactory
            {
                HostName = _configuration.Host,
                UserName = _configuration.Username,
                Password = _configuration.Password
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(
                        queue: _configuration.Queue,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

            _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} - Inicializou e configurou a fila");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (sender, eventArgs) =>
            {
                _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} - Recebeu uma mensagem");

                var contentArray = eventArgs.Body.ToArray();
                var contentString = Encoding.UTF8.GetString(contentArray);
                var message = JsonConvert.DeserializeObject<EmailInputModel>(contentString);

                _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} - Desserializou a mensagem");

                using (var scope = _serviceProvider.CreateScope())
                {
                    _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} - Iniciou o disparo do e-mail");

                    var notificationService = scope.ServiceProvider.GetRequiredService<INotificacaoService>();
                    var result = notificationService.Notificar(message.OrigemId, message.DestinoId, message.Conteudo) ? "Sucesso" : "Falha";

                    _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} - Finalizou o disparo ro e-mail com {result}");
                }

                _channel.BasicAck(eventArgs.DeliveryTag, false);

                _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} - Removeu mensagem da fila");
            };

            _channel.BasicConsume(_configuration.Queue, false, consumer);

            return Task.CompletedTask;
        }
    }
}
