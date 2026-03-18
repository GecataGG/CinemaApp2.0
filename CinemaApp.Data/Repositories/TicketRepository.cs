namespace CinemaApp.Data.Repositories
{
    using Contracts;
    using Models;

    public class TicketRepository : BaseRepository, ITicketRepository
    {
        public TicketRepository(CinemaAppDbContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<bool> AddTicketAsync(Ticket ticket, Guid projectionId)
        {
            await DbContext!.Tickets.AddAsync(ticket);

            Projection? projection = await DbContext
                .Projections
                .FindAsync(projectionId);
            if (projection == null)
            {
                throw new InvalidOperationException("Projection was not found.");
            }

            projection.AvailableTickets -= ticket.Quantity;

            int resultCount = await SaveChangesAsync();

            return resultCount >= 1;
        }
    }
}
