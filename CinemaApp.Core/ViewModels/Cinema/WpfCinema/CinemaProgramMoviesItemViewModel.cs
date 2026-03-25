namespace CinemaApp.Core.ViewModels.Cinema.WpfCinema
{
    using CinemaApp.Core.ViewModels.Shared;

    public class CinemaProgramMoviesItemViewModel : ViewModelBase
    {
        private Guid id;
        private string title = null!;
        private string director = null!;
        private string imageUrl = null!;

        public Guid Id
        {
            get => this.id;
            set => this.SetProperty(ref this.id, value);
        }

        public string Title
        {
            get => this.title;
            set => this.SetProperty(ref this.title, value);
        }

        public string Director
        {
            get => this.director;
            set => this.SetProperty(ref this.director, value);
        }

        public string ImageUrl
        {
            get => this.imageUrl;
            set => this.SetProperty(ref this.imageUrl, value);
        }
    }
}