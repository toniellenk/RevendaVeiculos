using RevendaVeiculos.Data.BaseRepository;
using RevendaVeiculos.Data.Entities;
using RevendaVeiculos.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevendaVeiculos.Service.Services.Veiculos
{
    public interface IVeiculosService : IBaseRepository<Veiculo>
    {
        Task<PagedQuery<Veiculo>> ListPagedAsync(int page, int pageSize);
        Task<Veiculo?> GetDetailsAsync(int? id);
        new Task<ServiceResult<Veiculo>> AddAsync(Veiculo entity);
        new Task<ServiceResult<Veiculo>> UpdateAsync(Veiculo entity);
    }
}
