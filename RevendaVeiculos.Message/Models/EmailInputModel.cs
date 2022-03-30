namespace RevendaVeiculos.Message.Models
{
    public class EmailInputModel
    {
        public int OrigemId { get; set; }
        public int DestinoId { get; set; }
        public string Conteudo { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }
}
