﻿using RevendaVeiculos.Data;
using RevendaVeiculos.Data.BaseRepository;
using RevendaVeiculos.Data.Entities;
using RevendaVeiculos.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevendaVeiculos.Service.Services.Marcas
{
    public class MarcasService : BaseRepository<Marca>, IMarcasService
    {
        public MarcasService(RevendaVeiculosContext context) : base(context)
        {
        }

        public Task<IEnumerable<Marca>> GetAllActiveListAsync() =>
                 ToListAsync(p => p.StatusRegistro == StatusRegistroEnum.Ativo);
    }
}
