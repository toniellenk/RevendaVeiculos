using Microsoft.EntityFrameworkCore;
using RevendaVeiculos.Data.BaseRepository;
using RevendaVeiculos.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace RevendaVeiculos.Data.Entities
{
    [Index(nameof(Nome), IsUnique = true)]
    public class Marca : BaseEntity
    {
        public string Nome { get; set; }

        [Display(Name = "Status")]
        public StatusRegistroEnum StatusRegistro { get; set; } = StatusRegistroEnum.Ativo;
    }
}
