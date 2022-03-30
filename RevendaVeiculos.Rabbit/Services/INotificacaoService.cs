namespace RevendaVeiculos.Rabbit.Services
{
    public interface INotificacaoService
    {
        void Notificar(int origemId, int destinoId, string conteudo);
    }
}
