namespace CinemaApp.Core.ViewModels.Cinema.WpfCinema
{
    using CinemaApp.Core.Interfaces.IServices;
    using CinemaApp.Core.ViewModels.Shared;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    public class CinemaProgramViewModel : ViewModelBase
    {
        private readonly ICinemaService cinemaService;
        private Guid cinemaId;
        private string cinemaName = null!;
        private ObservableCollection<CinemaProgramMoviesItemViewModel> projectionMovies;
        private bool isLoading;
        private string? errorMessage;

        public CinemaProgramViewModel(ICinemaService cinemaService)
        {
            this.cinemaService = cinemaService;
            this.projectionMovies = new ObservableCollection<CinemaProgramMoviesItemViewModel>();

            this.LoadProgramCommand = new RelayCommand(async () => await LoadProgramAsync());
            this.BackCommand = new RelayCommand(() => this.BackRequested?.Invoke(this, EventArgs.Empty));
        }

        public Guid CinemaId
        {
            get => this.cinemaId;
            set => this.SetProperty(ref this.cinemaId, value);
        }

        public string CinemaName
        {
            get => this.cinemaName;
            set => this.SetProperty(ref this.cinemaName, value);
        }

        public ObservableCollection<CinemaProgramMoviesItemViewModel> ProjectionMovies
        {
            get => this.projectionMovies;
            set => this.SetProperty(ref this.projectionMovies, value);
        }

        public bool IsLoading
        {
            get => this.isLoading;
            set => this.SetProperty(ref this.isLoading, value);
        }

        public string? ErrorMessage
        {
            get => this.errorMessage;
            set => this.SetProperty(ref this.errorMessage, value);
        }

        public ICommand LoadProgramCommand { get; }
        public ICommand BackCommand { get; }

        // Събития за навигация
        public event EventHandler? BackRequested;
        public event EventHandler<Guid>? MovieDetailsRequested;

        public async Task LoadProgramForCinemaAsync(Guid id)
        {
            this.CinemaId = id;
            await LoadProgramAsync();
        }

        private async Task LoadProgramAsync()
        {
            if (this.CinemaId == Guid.Empty)
                return;

            try
            {
                this.IsLoading = true;
                this.ErrorMessage = null;

                var program = await this.cinemaService.GetCinemaProgramByIdAsync(this.CinemaId);

                if (program == null)
                {
                    this.ErrorMessage = "Cinema not found.";
                    return;
                }

                this.CinemaName = program.Name;

                this.ProjectionMovies.Clear();
                foreach (var movie in program.ProjectionMovies)
                {
                    var movieVm = new CinemaProgramMoviesItemViewModel
                    {
                        Id = movie.Id,
                        Title = movie.Title,
                        Director = movie.Director,
                        ImageUrl = movie.ImageUrl
                    };

                    // Добавяме команда за детайли на филма
                    this.ProjectionMovies.Add(movieVm);
                }
            }
            catch (Exception ex)
            {
                this.ErrorMessage = $"Error loading cinema program: {ex.Message}";
            }
            finally
            {
                this.IsLoading = false;
            }
        }

        public void ShowMovieDetails(Guid movieId)
        {
            this.MovieDetailsRequested?.Invoke(this, movieId);
        }
    }
}