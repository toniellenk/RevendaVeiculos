using RevendaVeiculos.Data.Enums;
using LazZiya.ExpressLocalization.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace RevendaVeiculos.Web.Models
{
    public class MarcaVM
    {
        [ExRequired]
        public int Id { get; set; }

        [ExRequired]
        public string? Nome { get; set; }

        [ExRequired]
        [Display(Name = "Status")]
        public StatusRegistroEnum StatusRegistro { get; set; } = StatusRegistroEnum.Ativo;
    }
}
