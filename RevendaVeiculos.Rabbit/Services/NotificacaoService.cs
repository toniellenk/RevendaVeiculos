namespace RevendaVeiculos.Rabbit.Services
{
    public class NotificacaoService : INotificacaoService
    {
        public void Notificar(int origemId, int destinoId, string conteudo)
        {
            Console.WriteLine($"origemId: {origemId}");
            Console.WriteLine($"destinoId: {destinoId}");
            Console.WriteLine($"conteudo: {conteudo}");
        }
    }
}
