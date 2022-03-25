using RevendaVeiculos.Data.BaseRepository;
using RevendaVeiculos.Data.Entities;

namespace RevendaVeiculos.Service.Services.Proprietarios
{
    public interface IProprietariosService : IBaseRepository<Proprietario>
    {
        Task<IEnumerable<Proprietario>> GetAllActiveListAsync();
    }
}
