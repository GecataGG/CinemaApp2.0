namespace CinemaApp.Core.DTOs.Projection
{
    public class ProjectionShowtimeDto
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public int AvailableTickets { get; set; }
    }
}
