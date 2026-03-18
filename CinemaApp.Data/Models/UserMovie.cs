namespace CinemaApp.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations.Schema;

    [PrimaryKey(nameof(UserId), nameof(MovieId))]
    public class UserMovie
    {
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;

        public virtual IdentityUser User { get; set; } = null!;

        [ForeignKey(nameof(Movie))]
        public Guid MovieId { get; set; }

        public virtual Movie Movie { get; set; } = null!;

        public bool IsDeleted { get; set; } = false;
    }
}