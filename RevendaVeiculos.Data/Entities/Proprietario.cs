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
        public string Email { get; set; }
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public StatusRegistroEnum StatusRegistro { get; set; } = StatusRegistroEnum.Ativo;
    }
}
