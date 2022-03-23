using RevendaVeiculos.Data.Enums;

namespace RevendaVeiculos.Web.Models
{
    public class MarcaVM
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public StatusRegistroEnum StatusRegistro { get; set; } = StatusRegistroEnum.Ativo;
    }
}
