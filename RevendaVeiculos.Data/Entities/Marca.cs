using Microsoft.EntityFrameworkCore;
using RevendaVeiculos.Data.BaseRepository;
using RevendaVeiculos.Data.Enums;

namespace RevendaVeiculos.Data.Entities
{
    [Index(nameof(Nome), IsUnique = true)]
    public class Marca : BaseEntity
    {
        public string Nome { get; set; }
        public StatusRegistroEnum StatusRegistro { get; set; } = StatusRegistroEnum.Ativo;
    }
}
