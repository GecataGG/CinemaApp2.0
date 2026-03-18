namespace CinemaApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static Common.EntityValidation.Projection;

    public class Projection
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid CinemaId { get; set; }

        [ForeignKey(nameof(CinemaId))]
        public virtual Cinema Cinema { get; set; } = null!;

        [Required]
        public Guid MovieId { get; set; }

        [ForeignKey(nameof(MovieId))]
        public virtual Movie Movie { get; set; } = null!;

        public bool IsDeleted { get; set; } = false;

        public DateTime Showtime { get; set; }

        public int AvailableTickets { get; set; }

        [Column(TypeName = TicketPriceType)]
        public decimal TicketPrice { get; set; }

        [Timestamp]
        public byte[] Version { get; set; } = null!;

        public virtual ICollection<Ticket> Tickets { get; set; }
            = new HashSet<Ticket>();
    }
}