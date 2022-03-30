using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RevendaVeiculos.Rabbit.Consumers;
using RevendaVeiculos.Rabbit.Models;
using RevendaVeiculos.Rabbit.Services;

namespace RevendaVeiculos.Rabbit
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RabbitMqConfiguration>(Configuration.GetSection("RabbitMqConfig"));
            services.AddScoped<INotificacaoService, NotificacaoService>();

            services.AddHostedService<NotificacaoEmailConsumer>();
        }
    }
}
