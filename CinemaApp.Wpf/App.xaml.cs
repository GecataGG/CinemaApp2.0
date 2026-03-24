using CinemaApp.Core.Interfaces.IServices;
using CinemaApp.Core.Services;
using CinemaApp.Core.ViewModels.Cinema.WpfCinema;
using CinemaApp.Data;
using CinemaApp.Data.Repositories;
using CinemaApp.Data.Repositories.Contracts;
using CinemaApp.Wpf.ViewModels;
using CinemaApp.Wpf.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Windows;

namespace CinemaApp.Wpf
{
    public partial class App : Application
    {
         public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) 
                .AddJsonFile("appsettings.json") 
                .Build();

            string connectionString = config.GetConnectionString("SqlServer") 
                ?? throw new InvalidOperationException("Connection string not found."); ;

            var services = new ServiceCollection();

            services.AddDbContext<CinemaAppDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<ICinemaRepository, CinemaRepository>();
            services.AddScoped<ICinemaService, CinemaService>();

            // Добавяне на ViewModel и View
            services.AddTransient<LoginViewModel>();
            services.AddTransient<LoginView>();

            services.AddTransient<CinemaListViewModelWpf>();
            services.AddTransient<CinemaListWindow>();

            ServiceProvider = services.BuildServiceProvider();

            var window = ServiceProvider.GetRequiredService<LoginView>();
            window.Show();

            base.OnStartup(e);
        }
    }
}