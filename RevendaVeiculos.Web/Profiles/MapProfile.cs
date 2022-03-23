using AutoMapper;
using RevendaVeiculos.Data.Entities;
using RevendaVeiculos.Web.Models;

namespace RevendaVeiculos.Web.Profiles
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Marca, MarcaVM>();
        }
    }  
}
