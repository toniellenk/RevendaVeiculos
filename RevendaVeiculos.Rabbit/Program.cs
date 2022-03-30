﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RevendaVeiculos.Rabbit.Consumers;
using RevendaVeiculos.Rabbit.Models;
using RevendaVeiculos.Rabbit.Services;

namespace RevendaVeiculos.Rabbit
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
             .ConfigureServices((hostContext, services) =>
             {
                 IConfiguration configuration = hostContext.Configuration;

                 services.Configure<RabbitMqConfiguration>(configuration.GetSection("RabbitConfig"));

                 services.AddScoped<INotificacaoService, NotificacaoService>();

                 services.AddHostedService<NotificacaoEmailConsumer>();
             });
    }
}