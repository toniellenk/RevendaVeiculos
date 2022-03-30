namespace RevendaVeiculos.Message.Services
{
    public class NotificacaoService : INotificacaoService
    {
        public bool Notificar(int origemId, int destinoId, string conteudo)
        {
            Console.WriteLine($"origemId: {origemId}");
            Console.WriteLine($"destinoId: {destinoId}");
            Console.WriteLine($"conteudo: {conteudo}");

            return true;
        }
    }
}
