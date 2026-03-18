namespace CinemaApp.Core.ViewModels.Movie
{
    using System.ComponentModel.DataAnnotations;

    public class MovieFormModel
    {
        [Required(ErrorMessage = "Title is required.")]
        [MinLength(2, ErrorMessage = "Title must be at least 2 characters long.")]
        [MaxLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Genre is required.")]
        public string Genre { get; set; } = null!;

        [Required(ErrorMessage = "Director is required.")]
        [MinLength(2, ErrorMessage = "Director name must be at least 2 characters long.")]
        [MaxLength(100, ErrorMessage = "Director name cannot be longer than 100 characters.")]
        public string Director { get; set; } = null!;

        [Required(ErrorMessage = "Duration is required.")]
        [Range(1, 300, ErrorMessage = "Duration must be between 1 and 300 minutes.")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "Release date is required.")]
        public DateOnly ReleaseDate { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MinLength(10, ErrorMessage = "Description must be at least 10 characters long.")]
        [MaxLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters.")]
        public string Description { get; set; } = null!;

        [Url(ErrorMessage = "Please enter a valid image URL.")]
        [MaxLength(2048, ErrorMessage = "Image URL cannot be longer than 2048 characters.")]
        public string? ImageUrl { get; set; }
    }
}
