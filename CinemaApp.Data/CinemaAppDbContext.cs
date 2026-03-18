namespace CinemaApp.Data
{
    using CinemaApp.Data.Models;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class CinemaAppDbContext : IdentityDbContext
    {
        public CinemaAppDbContext(DbContextOptions<CinemaAppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; } = null!;
        public DbSet<Cinema> Cinemas { get; set; } = null!;
        public DbSet<Projection> Projections { get; set; } = null!;
        public DbSet<Ticket> Tickets { get; set; } = null!;
        public DbSet<UserMovie> UsersMovies { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(CinemaAppDbContext).Assembly);
        }
    }
}