using CinemaApp.Data;
using CinemaApp.Wpf.HelpersWpf;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace CinemaApp.Wpf.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private string email;
        private string password;
        private string confirmPassword;
        private readonly CinemaAppDbContext _context;

        // Команда за регистрация
        public ICommand RegisterCommand { get; }

        public RegisterViewModel(CinemaAppDbContext context)
        {
            _context = context;
            // Използваме RelayCommandWpf за създаване на командата
            RegisterCommand = new RelayCommandWpf(OnRegister, CanRegister);
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

        // Свойство за потвърдена парола
        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        // Проверка дали командата може да се изпълни (ако имейлът и паролата не са празни и паролите съвпадат)
        private bool CanRegister(object? parameter)
        {
            return !string.IsNullOrEmpty(Email) &&
                   !string.IsNullOrEmpty(Password) &&
                   Password == ConfirmPassword;
        }

        // Логика за регистрация
        private async void OnRegister(object? parameter)
        {
            // Проверка дали вече съществува потребител с този имейл
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == Email);

            if (existingUser != null)
            {
                MessageBox.Show("User already exists.");
                return;
            }

            // Създаване на нов потребител
            var newUser = new User
            {
                Email = Email,
                PasswordHash = Password, // За предпочитане паролата да бъде хеширана (не е препоръчително да се съхранява в чист текст)
            };

            // Добавяне на новия потребител в базата данни
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            MessageBox.Show("Registration successful!");
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