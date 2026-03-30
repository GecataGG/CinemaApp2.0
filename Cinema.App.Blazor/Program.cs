using CinemaApp.Core.Interfaces.IServices;
using CinemaApp.Core.Services;
using CinemaApp.Data;
using CinemaApp.Data.Repositories;
using CinemaApp.Data.Repositories.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Cinema.App.Blazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration
                .GetConnectionString("SqlServer")
                ?? throw new InvalidOperationException("Connection string 'SqlServer' not found.");

            builder.Services.AddDbContext<CinemaAppDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                ConfigureIdentity(builder.Configuration, options);
            })
            .AddEntityFrameworkStores<CinemaAppDbContext>();

            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddSession();

            builder.Services.AddScoped<IMovieRepository, MovieRepository>();
            builder.Services.AddScoped<ICinemaRepository, CinemaRepository>();
            builder.Services.AddScoped<IWatchlistRepository, WatchlistRepository>();
            builder.Services.AddScoped<IProjectionRepository, ProjectionRepository>();
            builder.Services.AddScoped<ITicketRepository, TicketRepository>();

            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddScoped<ICinemaService, CinemaService>();
            builder.Services.AddScoped<IWatchlistService, WatchlistService>();
            builder.Services.AddScoped<IProjectionService, ProjectionService>();
            builder.Services.AddScoped<ITicketService, TicketService>();

            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error", createScopeForErrors: true);
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAntiforgery();

            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorComponents<Components.App>()
                .AddInteractiveServerRenderMode();

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