namespace CinemaApp.Data.Repositories
{
    using System.Linq.Expressions;

    using Contracts;
    using Models;

    using Microsoft.EntityFrameworkCore;

    public class CinemaRepository : BaseRepository, ICinemaRepository
    {
        public CinemaRepository(CinemaAppDbContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<IEnumerable<Cinema>> GetAllCinemas(Expression<Func<Cinema, bool>>? filterQuery = null, Expression<Func<Cinema, Cinema>>? projectionQuery = null, bool includeProjections = false)
        {
            IQueryable<Cinema> cinemasFetchQuery = DbContext!
                .Cinemas
                .AsNoTracking();
            if (includeProjections)
            {
                cinemasFetchQuery = cinemasFetchQuery
                    .Include(c => c.Projections);
            }

            if (filterQuery != null)
            {
                cinemasFetchQuery = cinemasFetchQuery
                    .Where(filterQuery);
            }

            if (projectionQuery != null)
            {
                cinemasFetchQuery = cinemasFetchQuery
                    .Select(projectionQuery)
                    .AsQueryable();
            }

            IEnumerable<Cinema> result = await cinemasFetchQuery
                .ToArrayAsync();

            return result;
        }

        public async Task<Cinema?> GetCinemaByIdIncludeMovies(Guid cinemaId)
        {
            Cinema? cinema = await DbContext!
                .Cinemas
                .Include(c => c.Projections)
                .ThenInclude(p => p.Movie)
                .SingleOrDefaultAsync(c => c.Id == cinemaId);

            return cinema;
        }
    }
}
