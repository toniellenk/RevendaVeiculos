using RevendaVeiculos.Message.Consumers;
using RevendaVeiculos.Message.Models;
using RevendaVeiculos.Message.Services;

IHost host = Host.CreateDefaultBuilder(args)
             .ConfigureServices((hostContext, services) =>
             {
                 IConfiguration configuration = hostContext.Configuration;

                 services.Configure<RabbitMqConfiguration>(configuration.GetSection("RabbitConfig"));

                 services.AddScoped<INotificacaoService, NotificacaoService>();

                 services.AddHostedService<NotificacaoEmailConsumer>();
             })
            .Build();

await host.RunAsync();
