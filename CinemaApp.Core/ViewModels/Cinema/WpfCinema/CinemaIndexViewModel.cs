namespace CinemaApp.Core.ViewModels.Cinema.WpfCinema
{
    using CinemaApp.Core.Interfaces.IServices;
    using CinemaApp.Core.ViewModels.Shared;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    public class CinemaIndexViewModel : ViewModelBase
    {
        private readonly ICinemaService cinemaService;
        private ObservableCollection<CinemaItemViewModel> cinemas;
        private CinemaItemViewModel? selectedCinema;
        private bool isLoading;
        private string? errorMessage;

        public CinemaIndexViewModel(ICinemaService cinemaService)
        {
            this.cinemaService = cinemaService;
            this.cinemas = new ObservableCollection<CinemaItemViewModel>();

            this.LoadCinemasCommand = new RelayCommand(async () => await LoadCinemasAsync());
            this.ShowProgramCommand = new RelayCommand(async () => await ShowProgramAsync(), () => SelectedCinema != null);

            // Автоматично зареждане при създаване
            _ = LoadCinemasAsync();
        }

        public ObservableCollection<CinemaItemViewModel> Cinemas
        {
            get => this.cinemas;
            set => this.SetProperty(ref this.cinemas, value);
        }

        public CinemaItemViewModel? SelectedCinema
        {
            get => this.selectedCinema;
            set
            {
                if (this.SetProperty(ref this.selectedCinema, value))
                {
                    ((RelayCommand)this.ShowProgramCommand).RaiseCanExecuteChanged();
                }
            }
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

        public ICommand LoadCinemasCommand { get; }
        public ICommand ShowProgramCommand { get; }

        // Събитие за навигация (ще се handle-ва от View)
        public event EventHandler<Guid>? ShowProgramRequested;

        private async Task LoadCinemasAsync()
        {
            try
            {
                this.IsLoading = true;
                this.ErrorMessage = null;

                var cinemas = await this.cinemaService.GetAllCinemasOrderedByLocationAsync();

                this.Cinemas.Clear();
                foreach (var cinema in cinemas)
                {
                    this.Cinemas.Add(new CinemaItemViewModel
                    {
                        Id = cinema.Id,
                        Name = cinema.Name,
                        Location = cinema.Location
                    });
                }
            }
            catch (Exception ex)
            {
                this.ErrorMessage = $"Error loading cinemas: {ex.Message}";
            }
            finally
            {
                this.IsLoading = false;
            }
        }

        private async Task ShowProgramAsync()
        {
            if (this.SelectedCinema == null)
                return;

            // Повишаваме събитие за навигация към програмата
            this.ShowProgramRequested?.Invoke(this, this.SelectedCinema.Id);
        }
    }
}