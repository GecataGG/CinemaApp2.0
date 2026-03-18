namespace CinemaApp.Core.DTOs.Movie
{
    public class MovieDetailsDto : MovieAllDto
    {
        public string Description { get; set; } = null!;
        public int Duration { get; set; }
    }
}
