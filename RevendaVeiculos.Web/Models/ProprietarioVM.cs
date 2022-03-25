using LazZiya.ExpressLocalization.DataAnnotations;
using RevendaVeiculos.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace RevendaVeiculos.Web.Models
{
    public class ProprietarioVM
    {
        [ExRequired]
        public int Id { get; set; }

        [ExRequired]
        public string Nome { get; set; }

        [ExRequired]
        public string Documento { get; set; }

        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        [ExRequired]
        public string Email { get; set; }

        [Display(Name = "Endereço")]
        [ExRequired]
        public string Endereco { get; set; }

        [Display(Name = "Status")]
        [ExRequired]
        public StatusRegistroEnum StatusRegistro { get; set; } = StatusRegistroEnum.Ativo;
    }
}
