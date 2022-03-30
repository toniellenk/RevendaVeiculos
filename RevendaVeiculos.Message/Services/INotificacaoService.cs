namespace RevendaVeiculos.Message.Services
{
    public interface INotificacaoService
    {
        bool Notificar(int origemId, int destinoId, string conteudo);
    }
}
