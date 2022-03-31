using RevendaVeiculos.Message.Consumers;
using RevendaVeiculos.Message.Models;
using RevendaVeiculos.SendGrid.DependencyInjection;


IHost host = Host.CreateDefaultBuilder(args)
             .ConfigureServices((hostContext, services) =>
             {
                 IConfiguration configuration = hostContext.Configuration;

                 services.Configure<RabbitMqConfiguration>(configuration.GetSection("RabbitConfig"));

                 services.AddSendGridEmailService(configuration["SendGrid:ApiKey"],
                     options =>
                     {
                         options.FromEmail = configuration["Email:FromEmail"];
                         options.FromName = configuration["Email:FromName"];
                     });

                 services.AddHostedService<NotificacaoEmailConsumer>();
             })
            .Build();

await host.RunAsync();
