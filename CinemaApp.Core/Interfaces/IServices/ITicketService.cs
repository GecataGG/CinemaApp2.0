namespace CinemaApp.Core.Interfaces.IServices
{
    public interface ITicketService
    {
        public Task<bool> BuyTicketAsync(Guid projectionId, string userId, int quantity);
    }
}
