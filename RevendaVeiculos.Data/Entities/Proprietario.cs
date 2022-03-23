using Microsoft.EntityFrameworkCore;
using RevendaVeiculos.Data.BaseRepository;
using RevendaVeiculos.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace RevendaVeiculos.Data.Entities
{
    [Index(nameof(Documento), IsUnique = true)]
    public class Proprietario : BaseEntity
    {
        public string Nome { get; set; }
        public string Documento { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Display(Name = "Status")]
        public StatusRegistroEnum StatusRegistro { get; set; } = StatusRegistroEnum.Ativo;
    }
}
