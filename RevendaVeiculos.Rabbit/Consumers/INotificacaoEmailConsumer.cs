namespace RevendaVeiculos.Rabbit.Consumers
{
    public interface INotificacaoEmailConsumer
    {
        Task NotificarAsync();
    }
}
