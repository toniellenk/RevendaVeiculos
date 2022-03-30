#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RevendaVeiculos.Data.Entities;

namespace RevendaVeiculos.Data
{
    public class RevendaVeiculosContext : DbContext
    {
        public RevendaVeiculosContext()
        {
        }
        public RevendaVeiculosContext (DbContextOptions<RevendaVeiculosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Marca> Marca { get; set; }
        public virtual DbSet<Proprietario> Proprietario { get; set; }
        public virtual DbSet<Veiculo> Veiculo { get; set; }
    }
}
