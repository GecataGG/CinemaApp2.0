namespace CinemaApp.Data.Configurations
{
    using CinemaApp.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder
                .HasKey(m => m.Id);

            builder
                .Property(m => m.Title)
                .IsRequired();

            builder
                .Property(m => m.Genre)
                .IsRequired();

            builder
                .Property(m => m.Director)
                .IsRequired();

            builder
                .Property(m => m.Description)
                .IsRequired();

            builder
                .Property(m => m.IsDeleted)
                .HasDefaultValue(false);

            builder
                .HasMany(m => m.Projections)
                .WithOne(p => p.Movie)
                .HasForeignKey(p => p.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(m => m.MovieUsersWatchlist)
                .WithOne(um => um.Movie)
                .HasForeignKey(um => um.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasQueryFilter(m => !m.IsDeleted);

            builder.HasData(SeedMovies());
        }

        private static IEnumerable<Movie> SeedMovies()
        {
            return new List<Movie>
            {
                new Movie
                {
                    Id = Guid.Parse("ae50a5ab-9642-466f-b528-3cc61071bb4c"),
                    Title = "Harry Potter and the Goblet of Fire",
                    Genre = "Fantasy",
                    ReleaseDate = DateOnly.FromDateTime(new DateTime(2005, 11, 1)),
                    Director = "Mike Newell",
                    Duration = 157,
                    Description = "Harry Potter and the Goblet of Fire is a 2005 fantasy film directed by Mike Newell from a screenplay by Steve Kloves. It is based on the 2000 novel by J. K. Rowling.",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BMTI1NDMyMjExOF5BMl5BanBnXkFtZTcwOTc4MjQzMQ@@._V1_.jpg",
                    IsDeleted = false
                },
                new Movie
                {
                    Id = Guid.Parse("777634e2-3bb6-4748-8e91-7a10b70c78ac"),
                    Title = "Lord of the Rings",
                    Genre = "Fantasy",
                    ReleaseDate = DateOnly.FromDateTime(new DateTime(2001, 5, 1)),
                    Director = "Peter Jackson",
                    Duration = 178,
                    Description = "The Lord of the Rings: The Fellowship of the Ring is a 2001 epic high fantasy adventure film directed by Peter Jackson.",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BNzIxMDQ2YTctNDY4MC00ZTRhLTk4ODQtMTVlOWY4NTdiYmMwXkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg",
                    IsDeleted = false
                },
                new Movie
                {
                    Id = Guid.Parse("68fb84b9-ef2a-402f-b4fc-595006f5c275"),
                    Title = "Inception",
                    Genre = "Sci-Fi",
                    ReleaseDate = DateOnly.FromDateTime(new DateTime(2010, 7, 16)),
                    Director = "Christopher Nolan",
                    Duration = 148,
                    Description = "A thief who enters the dreams of others to steal secrets is given the inverse task: planting an idea into someone's mind.",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BMjAxMzY3NjcxNF5BMl5BanBnXkFtZTcwNTI5OTM0Mw@@._V1_.jpg",
                    IsDeleted = false
                },
                new Movie
                {
                    Id = Guid.Parse("02b52bb0-1c2b-49a4-ba66-6d33f81d38d1"),
                    Title = "The Dark Knight",
                    Genre = "Action",
                    ReleaseDate = DateOnly.FromDateTime(new DateTime(2008, 7, 18)),
                    Director = "Christopher Nolan",
                    Duration = 152,
                    Description = "Batman faces the Joker, who seeks to create chaos in Gotham through psychological warfare.",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BMTMxNTMwODM0NF5BMl5BanBnXkFtZTcwODAyMTk2Mw@@._V1_FMjpg_UX1000_.jpg",
                    IsDeleted = false
                },
                new Movie
                {
                    Id = Guid.Parse("16376cc6-b3e0-4bf7-a0e4-9cbd1490522c"),
                    Title = "Interstellar",
                    Genre = "Sci-Fi",
                    ReleaseDate = DateOnly.FromDateTime(new DateTime(2014, 11, 7)),
                    Director = "Christopher Nolan",
                    Duration = 169,
                    Description = "A group of explorers travel through a wormhole in space in search of a new habitable planet.",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BYzdjMDAxZGItMjI2My00ODA1LTlkNzItOWFjMDU5ZDJlYWY3XkEyXkFqcGc@._V1_.jpg",
                    IsDeleted = false
                }
            };
        }
    }
}