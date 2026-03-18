namespace CinemaApp.Data.Repositories
{
    public abstract class BaseRepository : IDisposable
    {
        private bool isDisposed = false;
        private readonly CinemaAppDbContext? dbContext;

        protected BaseRepository(CinemaAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        protected CinemaAppDbContext? DbContext
            => dbContext;

        protected async Task<int> SaveChangesAsync()
        {
            return await DbContext!.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    dbContext?.Dispose();
                }
            }
            isDisposed = true;
        }
    }
}
