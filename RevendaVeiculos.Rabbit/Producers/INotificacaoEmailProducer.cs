using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RevendaVeiculos.Rabbit.Models;
using System.Text;

namespace RevendaVeiculos.Rabbit.Producers
{
    public interface INotificacaoEmailProducer
    {
        bool PostNotificacao(EmailInputModel message);
    }
}
