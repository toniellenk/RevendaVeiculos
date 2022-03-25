using LazZiya.ExpressLocalization.DataAnnotations;
using RevendaVeiculos.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace RevendaVeiculos.Web.Models
{
    public class VeiculoVM
    {
        [ExRequired]
        public int Id { get; set; }

        [ExRequired]
        public string? Nome { get; set; }

        [ExRequired]
        [Display(Name = "Proprierário")]
        public int ProprietarioId { get; set; }
        public virtual ProprietarioVM? Proprietario { get; set; }

        [ExRequired]
        [Display(Name = "RENAVAM")]
        public string? Renavam { get; set; }

        [ExRequired]
        [Display(Name = "Marca")]
        public int MarcaId { get; set; }
        public virtual MarcaVM? Marca { get; set; }

        [ExRequired]
        public string? Modelo { get; set; }

        [Display(Name = "Ano Fabricação")]
        [ExRange(1000, 99999)]
        [ExRequired]
        public int AnoFabricacao { get; set; }

        [Display(Name = "Ano Modelo")]
        [ExRange(1000, 99999)]
        [ExRequired]
        public int AnoModelo { get; set; }

        [ExRequired]
        public int Quilometragem { get; set; }

        [ExRequired]
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }

        [Display(Name = "Status")]
        public StatusVeiculoEnum StatusVeiculo { get; set; } = StatusVeiculoEnum.Disponivel;
    }
}
