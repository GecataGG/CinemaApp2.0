namespace CinemaApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidation.Cinema;

    public class Cinema
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(LocationMaxLength)]
        public string Location { get; set; } = null!;

        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<Projection> Projections { get; set; }
            = new HashSet<Projection>();
    }
}