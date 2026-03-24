using CinemaApp.Core.Interfaces.IServices;
using CinemaApp.Core.ViewModels.Cinema;
using CinemaApp.Core.ViewModels.Cinema.WpfCinema;
using System.Windows;

namespace CinemaApp.Wpf.Views
{
    public partial class CinemaListWindow : Window
    {
        public CinemaListWindow(ICinemaService cinemaService)
        {
            InitializeComponent();

            DataContext = new CinemaListViewModelWpf(cinemaService);
        }
    }
}