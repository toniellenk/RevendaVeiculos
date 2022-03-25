using RevendaVeiculos.Data;
using RevendaVeiculos.Data.BaseRepository;
using RevendaVeiculos.Data.Entities;
using RevendaVeiculos.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevendaVeiculos.Service.Services.Proprietarios
{
    public class ProprietariosService : BaseRepository<Proprietario>, IProprietariosService
    {
        public ProprietariosService(RevendaVeiculosContext context) : base(context)
        {
        }

        public Task<IEnumerable<Proprietario>> GetAllActiveListAsync() =>
            ToListAsync(p => p.StatusRegistro == StatusRegistroEnum.Ativo);

    }
}
