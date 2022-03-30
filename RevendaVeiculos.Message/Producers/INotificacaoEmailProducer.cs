using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RevendaVeiculos.Message.Models;
using System.Text;

namespace RevendaVeiculos.Message.Producers
{
    public interface INotificacaoEmailProducer
    {
        bool PostNotificacao(EmailInputModel message);
    }
}
