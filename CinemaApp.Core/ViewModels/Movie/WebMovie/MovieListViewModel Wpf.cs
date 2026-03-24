using CinemaApp.Core.Interfaces.IServices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CinemaApp.Core.ViewModels.Movie.WebMovie
{
    public class MovieListViewModel : INotifyPropertyChanged
    {
        private readonly IMovieService movieService;

        public ObservableCollection<AllMoviesIndexViewModel> Movies { get; set; }
            = new ObservableCollection<AllMoviesIndexViewModel>();

        public ICommand LoadMoviesCommand { get; }

        public MovieListViewModel(IMovieService movieService)
        {
            this.movieService = movieService;
            LoadMoviesCommand = new RelayCommand(async _ => await LoadMoviesAsync());
        }

        // Зареждане на филми
        public async Task LoadMoviesAsync()
        {
            var movieDtos = await movieService.GetAllMoviesOrderedByTitleAsync();

            Movies.Clear();

            foreach (var movie in movieDtos)
            {
                Movies.Add(new AllMoviesIndexViewModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Director = movie.Director,
                    Genre = movie.Genre,
                    ReleaseDate = movie.ReleaseDate.ToString("yyyy-MM-dd"),
                    ImageUrl = movie.ImageUrl,
                    IsInUserWatchlist = movie.IsInUserWatchlist
                });
            }
        }

        // PropertyChanged за WPF Binding
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    // RelayCommand за WPF
    public class RelayCommand : ICommand
    {
        private readonly Action<object?> execute;
        private readonly Func<object?, bool> canExecute;

        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute ?? (_ => true);
        }

        public bool CanExecute(object? parameter) => this.canExecute(parameter);
        public void Execute(object? parameter) => this.execute(parameter);
        public event EventHandler? CanExecuteChanged;
    }
}