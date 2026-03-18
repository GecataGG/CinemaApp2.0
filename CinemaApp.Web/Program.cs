using CinemaApp.Core.Interfaces.IServices;
using CinemaApp.Core.Services;
using CinemaApp.Data;
using CinemaApp.Data.Repositories;
using CinemaApp.Data.Repositories.Contracts;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration
                .GetConnectionString("SqlServer")
                ?? throw new InvalidOperationException("Connection string not found.");

            builder.Services.AddDbContext<CinemaAppDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services
                .AddDefaultIdentity<IdentityUser>(options =>
                {
                    ConfigureIdentity(builder.Configuration, options);
                })
                .AddEntityFrameworkStores<CinemaAppDbContext>();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            // Repositories
            builder.Services.AddScoped<IMovieRepository, MovieRepository>();
            builder.Services.AddScoped<ICinemaRepository, CinemaRepository>();
            builder.Services.AddScoped<IWatchlistRepository, WatchlistRepository>();
            builder.Services.AddScoped<IProjectionRepository, ProjectionRepository>();
            builder.Services.AddScoped<ITicketRepository, TicketRepository>();


            // Services
            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddScoped<ICinemaService, CinemaService>();
            builder.Services.AddScoped<IWatchlistService, WatchlistService>();
            builder.Services.AddScoped<IProjectionService, ProjectionService>();
            builder.Services.AddScoped<ITicketService, TicketService>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }

        private static void ConfigureIdentity(
            ConfigurationManager configuration,
            IdentityOptions options)
        {
            options.SignIn.RequireConfirmedAccount = configuration
                .GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");

            options.SignIn.RequireConfirmedEmail = configuration
                .GetValue<bool>("Identity:SignIn:RequireConfirmedEmail");

            options.SignIn.RequireConfirmedPhoneNumber = configuration
                .GetValue<bool>("Identity:SignIn:RequireConfirmedPhoneNumber");

            options.Password.RequireDigit = configuration
                .GetValue<bool>("Identity:Password:RequireDigit");

            options.Password.RequiredLength = configuration
                .GetValue<int>("Identity:Password:RequiredLength");

            options.Password.RequiredUniqueChars = configuration
                .GetValue<int>("Identity:Password:RequiredUniqueChars");

            options.Password.RequireLowercase = configuration
                .GetValue<bool>("Identity:Password:RequireLowercase");

            options.Password.RequireNonAlphanumeric = configuration
                .GetValue<bool>("Identity:Password:RequireNonAlphanumeric");

            options.Password.RequireUppercase = configuration
                .GetValue<bool>("Identity:Password:RequireUppercase");
        }
    }
}