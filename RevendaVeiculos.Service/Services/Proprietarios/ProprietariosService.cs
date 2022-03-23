using RevendaVeiculos.Data;
using RevendaVeiculos.Data.BaseRepository;
using RevendaVeiculos.Data.Entities;
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
    }
}
