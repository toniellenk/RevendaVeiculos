namespace RevendaVeiculos.Data.BaseRepository
{
    public class PagedQuery<T> where T : class
    {
        public int CurrentPage { get; set; }
        public int Total { get; set; }
        public int PageSize { get; set; }
        public int LineCounts { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}