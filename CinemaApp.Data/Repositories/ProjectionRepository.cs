namespace CinemaApp.Data.Repositories
{
    using System.Linq.Expressions;
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class ProjectionRepository : BaseRepository, IProjectionRepository
    {
        public ProjectionRepository(CinemaAppDbContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<IEnumerable<Projection>> GetAllProjectionsAsync(Expression<Func<Projection, bool>>? filterQuery = null)
        {
            IQueryable<Projection> projectionsFetchQuery = this.DbContext!
                .Projections
                .AsNoTracking();
            if (filterQuery != null)
            {
                projectionsFetchQuery = projectionsFetchQuery.Where(filterQuery);
            }

            IEnumerable<Projection> projections = await projectionsFetchQuery
                .ToArrayAsync();

            return projections;
        }

        public async Task<Projection?> FindByIdAsync(Guid id)
        {
            return await DbContext!
                .Projections
                .FindAsync(id);
        }

        public async Task<bool> EditProjectionAsync(Projection projection)
        {
            DbContext!.Projections.Update(projection);
            int resultCount = await SaveChangesAsync();

            return resultCount == 1;
        }
    }
}
