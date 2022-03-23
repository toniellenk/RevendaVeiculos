using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevendaVeiculos.Data.BaseRepository
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
