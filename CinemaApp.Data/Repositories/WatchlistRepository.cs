namespace CinemaApp.Data.Repositories
{
    using System.Linq.Expressions;

    using Contracts;
    using Models;

    using Microsoft.EntityFrameworkCore;

    public class WatchlistRepository : BaseRepository, IWatchlistRepository
    {
        public WatchlistRepository(CinemaAppDbContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<IEnumerable<UserMovie>> GetAllUserMoviesAsync(Expression<Func<UserMovie, bool>>? filterQuery = null)
        {
            IQueryable<UserMovie> userMovies = DbContext!
                .UsersMovies
                .AsNoTracking()
                .Include(um => um.Movie);
            if (filterQuery != null)
            {
                userMovies = userMovies.Where(filterQuery);
            }

            return await userMovies.ToArrayAsync();
        }

        public async Task<UserMovie?> GetUserMovieAsync(string userId, Guid movieId)
        {
            UserMovie? userMovie = await DbContext!
                .UsersMovies
                .SingleOrDefaultAsync(um => um.UserId == userId &&
                                            um.MovieId == movieId);

            return userMovie;
        }

        public async Task<UserMovie?> GetUserMovieIncludeDeletedAsync(string userId, Guid movieId)
        {
            UserMovie? userMovie = await DbContext!
                .UsersMovies
                .IgnoreQueryFilters()
                .SingleOrDefaultAsync(um => um.UserId == userId &&
                                            um.MovieId == movieId);

            return userMovie;
        }

        public async Task<bool> ExistsAsync(string userId, Guid movieId)
        {
            bool watchListEntryExist = await DbContext!
                .UsersMovies

                .AnyAsync(um => um.UserId == userId && um.MovieId == movieId);

            return watchListEntryExist;
        }

        public async Task<bool> AddUserMovieAsync(UserMovie userMovie)
        {
            await DbContext!.UsersMovies.AddAsync(userMovie);
            int resultCount = await SaveChangesAsync();

            return resultCount == 1;
        }

        public async Task<bool> UpdateUserMovieAsync(UserMovie userMovie)
        {
            DbContext!.UsersMovies.Update(userMovie);

            int resultCount = await SaveChangesAsync();

            return resultCount == 1;
        }

        public async Task<bool> SoftDeleteUserMovieAsync(UserMovie userMovie)
        {
            userMovie.IsDeleted = true;
            DbContext!.UsersMovies.Update(userMovie);

            int resultCount = await SaveChangesAsync();

            return resultCount == 1;
        }
    }
}
