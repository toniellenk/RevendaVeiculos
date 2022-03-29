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

        [Display(Name = "CEP")]
        [ExRequired]
        [RegularExpression(@"^\s*\d{8}$", ErrorMessage = "Somente números e com 8 dígitos. Ex: 78088000")]
        public string Cep { get; set; }

        [Display(Name = "Endereço")]
        [ExRequired]
        public string Endereco { get; set; }

        [Display(Name = "Status")]
        [ExRequired]
        public StatusRegistroEnum StatusRegistro { get; set; } = StatusRegistroEnum.Ativo;
    }
}
