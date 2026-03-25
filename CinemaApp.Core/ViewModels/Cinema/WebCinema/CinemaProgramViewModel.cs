namespace CinemaApp.Core.ViewModels.Cinema.WebCinema
{
    using CinemaApp.Core.DTOs.Cinema;

    public class CinemaProgramViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<CinemaProgramMoviesViewModel> ProjectionMovies { get; set; }
            = new List<CinemaProgramMoviesViewModel>();

        // Статичен метод за мапване от DTO към ViewModel
        //public static CinemaProgramViewModel FromDto(CinemaProgramDetailsDto dto)
        //{
        //    return new CinemaProgramViewModel
        //    {
        //        Id = dto.Id,
        //        Name = dto.Name,
        //        ProjectionMovies = dto.ProjectionMovies
        //            .Select(CinemaProgramMoviesViewModel.FromDto)
        //            .ToList()
        //    };
        //}
    }
}