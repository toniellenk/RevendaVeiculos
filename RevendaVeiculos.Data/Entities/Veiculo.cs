using Microsoft.EntityFrameworkCore;
using RevendaVeiculos.Data.BaseRepository;
using RevendaVeiculos.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace RevendaVeiculos.Data.Entities
{
    [Index(nameof(Renavam), IsUnique = true)]
    public class Veiculo : BaseEntity
    {
        public string Nome { get; set; }
        public int ProprietarioId { get; set; }
        public virtual Proprietario Proprietario { get; set; }
        public string Renavam { get; set; }
        public int MarcaId { get; set; }
        public virtual Marca Marca { get; set; }
        public string Modelo { get; set; }
        public int AnoFabricacao { get; set; }
        public int AnoModelo { get; set; }
        public int Quilometragem { get; set; }
        public decimal Valor { get; set; }
        public StatusVeiculoEnum StatusVeiculo { get; set; } = StatusVeiculoEnum.Disponivel;
    }
}
