namespace CinemaApp.Core.DTOs.Cinema
{
    public class CinemaProgramDetailsDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public IEnumerable<CinemaProgramMovieDto> ProjectionMovies { get; set; } = new List<CinemaProgramMovieDto>();
    }
}