namespace RevendaVeiculos.Data.BaseRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly RevendaVeiculosContext _context;

        public UnitOfWork(RevendaVeiculosContext context)
        {
            _context = context;
        }

        public int Commit()
            => _context.SaveChanges();

        public async Task<int> CommitAsync()
            => await _context.SaveChangesAsync();

        public void Dispose()
            => _context.Dispose();
    }
}
