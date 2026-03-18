namespace CinemaApp.Data.Configurations
{
    using CinemaApp.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserMovieConfiguration : IEntityTypeConfiguration<UserMovie>
    {
        public void Configure(EntityTypeBuilder<UserMovie> builder)
        {
            builder
                .HasKey(um => new { um.UserId, um.MovieId });

            builder
                .Property(um => um.IsDeleted)
                .HasDefaultValue(false);

            builder
                .HasOne(um => um.User)
                .WithMany()
                .HasForeignKey(um => um.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(um => um.Movie)
                .WithMany(m => m.MovieUsersWatchlist)
                .HasForeignKey(um => um.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasQueryFilter(um => !um.IsDeleted);
        }
    }
}