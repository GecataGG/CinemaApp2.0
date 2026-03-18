namespace CinemaApp.Data.Repositories.Contracts
{
    using CinemaApp.Data.Models;

    public interface ITicketRepository
    {
        Task<bool> AddTicketAsync(Ticket ticket, Guid projectionId);
    }
}