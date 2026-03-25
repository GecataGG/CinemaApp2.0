namespace CinemaApp.Core.ViewModels.Cinema.WpfCinema
{
    using CinemaApp.Core.ViewModels.Shared;

    public class CinemaItemViewModel : ViewModelBase
    {
        private Guid id;
        private string name = null!;
        private string location = null!;

        public Guid Id
        {
            get => this.id;
            set => this.SetProperty(ref this.id, value);
        }

        public string Name
        {
            get => this.name;
            set => this.SetProperty(ref this.name, value);
        }

        public string Location
        {
            get => this.location;
            set => this.SetProperty(ref this.location, value);
        }
    }
}