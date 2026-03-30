using CinemaApp.Core.Interfaces.IServices;
using CinemaApp.Core.Services;
using CinemaApp.Data;
using CinemaApp.Data.Repositories;
using CinemaApp.Data.Repositories.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Cinema.App.RazorPages
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
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
            })
            .AddEntityFrameworkStores<CinemaAppDbContext>();

            //behavior of the app
            builder.Services.AddRazorPages(options =>
            {
                options.Conventions.AddPageRoute("/Home/Index", "");
                options.Conventions.AuthorizePage("/Watchlist/Index");
                options.Conventions.AuthorizePage("/Movies/Create");
                options.Conventions.AuthorizePage("/Movies/Edit");
                options.Conventions.AuthorizePage("/Movies/Delete");
            });

           
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
                options.AccessDeniedPath = "/Home/Error";
                options.ReturnUrlParameter = "returnUrl"; //after login, redirect to the page the user wanted to access
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                options.SlidingExpiration = true; //activity extends the expiration time
            });


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
                app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");
                app.UseHsts();
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.MapRazorPages();

            app.MapGet("/", async context =>
            {
                context.Response.Redirect("/Home/Index");
                await Task.CompletedTask;
            });

            app.Run();
        }
    }
}