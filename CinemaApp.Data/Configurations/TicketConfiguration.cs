namespace CinemaApp.Data.Configurations
{
    using CinemaApp.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder
                .HasKey(t => t.Id);

            builder
                .Property(t => t.Quantity)
                .IsRequired();

            builder
                .HasOne(t => t.Projection)
                .WithMany(p => p.Tickets)
                .HasForeignKey(t => t.ProjectionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}