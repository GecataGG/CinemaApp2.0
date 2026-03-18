namespace CinemaApp.Core.ViewModels.Ticket
{
    public class BuyTicketInputModel
    {
        public Guid CinemaId { get; set; }
        public Guid MovieId { get; set; }
        public int Quantity { get; set; }
        public Guid ProjectionId { get; set; }
    }
}
