namespace RevendaVeiculos.Message.Models
{
    public class EmailInputModel
    {
        public string EmailDestino { get; set; }
        public string NomeDestino { get; set; }
        public string Conteudo { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }
}
