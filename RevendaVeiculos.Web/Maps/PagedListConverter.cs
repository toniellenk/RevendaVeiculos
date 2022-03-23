using AutoMapper;
using RevendaVeiculos.Data.BaseRepository;

namespace RevendaVeiculos.Web.Maps
{
    public class PagedListConverter<TSource, TDestination> : ITypeConverter<PagedQuery<TSource>, PagedQuery<TDestination>> where TSource : class where TDestination : class
    {
        public PagedQuery<TDestination> Convert(PagedQuery<TSource> source, PagedQuery<TDestination> destination, ResolutionContext context)
        {
            var collection = context.Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source.Data);

            return new PagedQuery<TDestination>(collection, source.CurrentPage, source.PageSize, source.Total);
        }
    }
}
