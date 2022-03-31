using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RevendaVeiculos.Message.Models;
using RevendaVeiculos.SendGrid.Abstractions;
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
        private readonly IEmailService _emailService;


        public NotificacaoEmailConsumer(ILogger<NotificacaoEmailConsumer> logger,
            IOptions<RabbitMqConfiguration> option,
            IServiceProvider serviceProvider,
            IEmailService emailService)
        {
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
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

            consumer.Received += async (sender, eventArgs) =>
            {
                _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} - Recebeu uma mensagem");

                var contentArray = eventArgs.Body.ToArray();
                var contentString = Encoding.UTF8.GetString(contentArray);
                var message = JsonConvert.DeserializeObject<EmailInputModel>(contentString);

                _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} - Desserializou a mensagem");


                try
                {
                    await _emailService.SendAsync(message.EmailDestino, message.NomeDestino, "Notificação - Venda Veículo", message.Conteudo, $"<b>{message.Conteudo}<b>", stoppingToken);
                    _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} - Finalizou o disparo ro e-mail com Sucesso");

                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} - Finalizou o disparo ro e-mail com Erro");
                    _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} - {ex.Message}");
                }

                _channel.BasicAck(eventArgs.DeliveryTag, false);

                _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} - Removeu mensagem da fila");
            };

            _channel.BasicConsume(_configuration.Queue, false, consumer);

            return Task.CompletedTask;
        }
    }
}
