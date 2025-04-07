using FinalProject_Core.Models;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Profiles;
using FinalProject_Service.Services.Implementations;
using FinalProject_Service.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_Presentation
{
    public static class SeviceRegistration
    {
        public static void Register(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddDbContext<TicsTubeDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<EmailService>();
            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequiredLength = 8;
                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedEmail = true;
                opt.Lockout.MaxFailedAccessAttempts = 5;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                opt.Lockout.AllowedForNewUsers = true;
            }).AddEntityFrameworkStores<TicsTubeDbContext>().AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(opt =>
            {
                opt.Events.OnRedirectToLogin = opt.Events.OnRedirectToAccessDenied = context =>
                {
                    var uri = new Uri(context.RedirectUri);
                    if (context.Request.Path.Value.ToLower().StartsWith("/admin"))
                    {

                        context.Response.Redirect("/admin/account/login" + uri.Query);
                    }
                    else
                    {
                        context.Response.Redirect("/account/login" + uri.Query);
                    }

                    return Task.CompletedTask;
                };
            });
            services.AddAutoMapper(opt =>
            {
                opt.AddProfile(new MapperProfile());
            });
            services.AddScoped<IActorService, ActorService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<IDirectorService, DirectorService>();
            services.AddScoped<IMovieService, MovieService>();
        }
    }
}
