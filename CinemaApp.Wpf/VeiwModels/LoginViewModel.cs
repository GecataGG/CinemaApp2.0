using CinemaApp.Data;
using CinemaApp.Wpf.HelpersWpf;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace CinemaApp.Wpf.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string email;
        private string password;
         private readonly CinemaAppDbContext _context;

        // Команда за логин
        public ICommand LoginCommand { get; }

        public LoginViewModel(CinemaAppDbContext context)
        {
            _context = context;
            // Използваме RelayCommandWpf за създаване на командата
            LoginCommand = new RelayCommandWpf(OnLogin, CanLogin);
        }

        // Свойство за имейл
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        // Свойство за парола
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        // Проверка дали командата може да се изпълни (ако имейлът и паролата не са празни)
        private bool CanLogin(object? parameter)
        {
            return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
        }

        // Логика за логин (асинхронен метод, използващ Entity Framework)
        private async void OnLogin(object? parameter)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == Email);

            if (user != null && user.PasswordHash == Password)
            {
                MessageBox.Show("Login successful!");
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        // Събитие за PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        // Известяване при промяна на свойствата
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}