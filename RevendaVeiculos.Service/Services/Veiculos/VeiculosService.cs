using Microsoft.EntityFrameworkCore;
using RevendaVeiculos.Data;
using RevendaVeiculos.Data.BaseRepository;
using RevendaVeiculos.Data.Entities;
using RevendaVeiculos.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevendaVeiculos.Service.Services.Veiculos
{
    public class VeiculosService : BaseRepository<Veiculo>, IVeiculosService
    {
        public VeiculosService(RevendaVeiculosContext context) : base(context)
        {
        }

        public async Task<PagedQuery<Veiculo>> ListPagedAsync(int page, int pageSize)
        {
            var query = _context.Veiculo
                            .Include(m => m.Marca)
                            .Include(p => p.Proprietario);

            return await Task.Run(() => query.OrderBy(i => i.Id).ToPagedQuery(page, pageSize));
        }

        public async Task<Veiculo?> GetDetailsAsync(int? id)
        {
            var query = _context.Veiculo
                   .Include(m => m.Marca)
                   .Include(p => p.Proprietario);

            return await query.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
