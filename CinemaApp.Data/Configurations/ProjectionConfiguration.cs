namespace CinemaApp.Data.Configurations
{
    using CinemaApp.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProjectionConfiguration : IEntityTypeConfiguration<Projection>
    {
        public void Configure(EntityTypeBuilder<Projection> builder)
        {
            builder
                .HasKey(p => p.Id);

            builder
                .Property(p => p.Id)
                .ValueGeneratedNever();

            builder
                .Property(p => p.IsDeleted)
                .HasDefaultValue(false);

            builder
                .Property(p => p.AvailableTickets)
                .IsRequired();

            builder
                .Property(p => p.Showtime)
                .IsRequired();

            builder
                .Property(p => p.TicketPrice)
                .IsRequired();

            builder
                .Property(p => p.Version)
                .IsRowVersion();

            builder
                .HasOne(p => p.Cinema)
                .WithMany(c => c.Projections)
                .HasForeignKey(p => p.CinemaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(p => p.Movie)
                .WithMany(m => m.Projections)
                .HasForeignKey(p => p.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(p => p.Tickets)
                .WithOne(t => t.Projection)
                .HasForeignKey(t => t.ProjectionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasIndex(p => new { p.CinemaId, p.MovieId, p.Showtime })
                .IsUnique()
                .HasDatabaseName("IX_CinemaMovieShowtime_Unique");

            builder
                .HasQueryFilter(p => !p.IsDeleted);

            builder.HasData(SeedProjections());
        }

        private static IEnumerable<Projection> SeedProjections()
        {
            return new List<Projection>
            {
                new Projection
                {
                    Id = Guid.Parse("1d696303-8c43-406c-a007-c142ac1986af"),
                    CinemaId = Guid.Parse("86e9d655-4bec-4685-b42f-40f93efedda2"),
                    MovieId = Guid.Parse("ae50a5ab-9642-466f-b528-3cc61071bb4c"),
                    Showtime = new DateTime(2026, 3, 3, 16, 30, 0),
                    AvailableTickets = 100,
                    TicketPrice = 10.00m,
                    IsDeleted = false
                },
                new Projection
                {
                    Id = Guid.Parse("9a4c71a6-660a-4932-95ba-ab8081f788ab"),
                    CinemaId = Guid.Parse("86e9d655-4bec-4685-b42f-40f93efedda2"),
                    MovieId = Guid.Parse("777634e2-3bb6-4748-8e91-7a10b70c78ac"),
                    Showtime = new DateTime(2026, 3, 5, 13, 45, 0),
                    AvailableTickets = 100,
                    TicketPrice = 10.00m,
                    IsDeleted = false
                },
                new Projection
                {
                    Id = Guid.Parse("edbb7f8d-95e9-4d1a-97da-d88e2b99fff1"),
                    CinemaId = Guid.Parse("ccb61fcf-9bd8-4008-88e8-dc69c9d24566"),
                    MovieId = Guid.Parse("ae50a5ab-9642-466f-b528-3cc61071bb4c"),
                    Showtime = new DateTime(2026, 3, 8, 19, 0, 0),
                    AvailableTickets = 80,
                    TicketPrice = 12.50m,
                    IsDeleted = false
                },
                new Projection
                {
                    Id = Guid.Parse("4edcb701-2070-4570-8700-c52674e39427"),
                    CinemaId = Guid.Parse("e2e63228-9ddf-491c-888a-f8077c53430e"),
                    MovieId = Guid.Parse("68fb84b9-ef2a-402f-b4fc-595006f5c275"),
                    Showtime = new DateTime(2026, 3, 9, 11, 20, 0),
                    AvailableTickets = 120,
                    TicketPrice = 15.00m,
                    IsDeleted = false
                }
            };
        }
    }
}