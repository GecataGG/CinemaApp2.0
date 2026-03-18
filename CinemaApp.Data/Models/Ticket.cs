namespace CinemaApp.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static Common.EntityValidation.Ticket;

    public class Ticket
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid ProjectionId { get; set; }

        [ForeignKey(nameof(ProjectionId))]
        public virtual Projection Projection { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public virtual IdentityUser User { get; set; } = null!;

        [Range(MinQuantity, MaxQuantity)]
        public int Quantity { get; set; }
    }
}