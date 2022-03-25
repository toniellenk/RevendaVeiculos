using AutoMapper;
using RevendaVeiculos.Data.BaseRepository;
using RevendaVeiculos.Data.Entities;
using RevendaVeiculos.Web.Models;

namespace RevendaVeiculos.Web.Maps
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap(typeof(PagedQuery<>), typeof(PagedQuery<>)).ConvertUsing(typeof(PagedListConverter<,>));

            CreateMap<Marca, MarcaVM>();
            CreateMap<MarcaVM, Marca>();

            CreateMap<Proprietario, ProprietarioVM>();
            CreateMap<ProprietarioVM, Proprietario>();

            CreateMap<Veiculo, VeiculoVM>();
            CreateMap<VeiculoVM, Veiculo>();
        }
    }  
}
