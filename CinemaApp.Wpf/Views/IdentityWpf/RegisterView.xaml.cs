using CinemaApp.Wpf.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace CinemaApp.Wpf.Views.IdentityWpf
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : Window
    {
        public RegisterView()
        {
            InitializeComponent();
            DataContext = App.ServiceProvider.GetRequiredService<LoginViewModel>();
        }
    }
}
