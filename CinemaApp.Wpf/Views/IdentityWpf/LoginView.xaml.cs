using CinemaApp.Wpf.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace CinemaApp.Wpf.Views
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            // Свързване на DataContext с LoginViewModel
            DataContext = App.ServiceProvider.GetRequiredService<LoginViewModel>();
        }

        // Обработчик на събитието PasswordChanged
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (passwordBox != null)
            {
                // Присвояваме стойността на паролата в ViewModel
                var viewModel = DataContext as LoginViewModel;
                if (viewModel != null)
                {
                    viewModel.Password = passwordBox.Password;  // Обновяваме Password в ViewModel
                }
            }
        }
    }
}
