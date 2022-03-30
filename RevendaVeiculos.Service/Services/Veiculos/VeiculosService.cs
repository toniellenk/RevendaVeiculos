using Microsoft.EntityFrameworkCore;
using RevendaVeiculos.Data;
using RevendaVeiculos.Data.BaseRepository;
using RevendaVeiculos.Data.Entities;
using RevendaVeiculos.Data.Enums;
using RevendaVeiculos.Service.Base;
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

        public new async Task<ServiceResult<Veiculo>> AddAsync(Veiculo entityInput)
        {
            var serviceResult = new ServiceResult<Veiculo>();

            if (await Any(r => r.Renavam == entityInput.Renavam))
                serviceResult.Erros.Add("Renavam", "Este RENAVAM já está vinculado à um veículo.");

            if (serviceResult.PossueErro)
                return serviceResult;

            _context.Set<Veiculo>().Add(entityInput);
            await _context.SaveChangesAsync();
            serviceResult.Result = entityInput;

            return serviceResult;
        }

        public new async Task<ServiceResult<Veiculo>> UpdateAsync(Veiculo entityInput)
        {
            var serviceResult = new ServiceResult<Veiculo>();

            if (await Any(r => r.Id != entityInput.Id && r.Renavam == entityInput.Renavam))
                serviceResult.Erros.Add("Renavam", "Este RENAVAM já está vinculado à um veículo.");

            if (await Any(r => entityInput.StatusVeiculo == StatusVeiculoEnum.Disponivel
                                    && r.StatusVeiculo != StatusVeiculoEnum.Disponivel
                                    && r.Id == entityInput.Id))
                serviceResult.Erros.Add("StatusVeiculo", "Não é permitido voltar o status para Disponível");

            if (serviceResult.PossueErro)
                return serviceResult;

            _context.Set<Veiculo>().Update(entityInput);
            await _context.SaveChangesAsync();
            serviceResult.Result = entityInput;

            return serviceResult;
        }
    }
}
