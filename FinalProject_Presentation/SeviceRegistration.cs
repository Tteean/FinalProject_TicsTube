using CloudinaryDotNet;
using FinalProject_Core.Models;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Extentions;
using FinalProject_Service.Profiles;
using FinalProject_Service.Services.Implementations;
using FinalProject_Service.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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
            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = "480081122868-gkakhva6sk5s2shkspbcb139uhhg1fju.apps.googleusercontent.com";
                options.ClientSecret = "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";
                options.SaveTokens = true;
            });
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

            services.AddScoped<Cloudinary>(sp =>
            {
                var config = sp.GetRequiredService<IOptions<CloudinarySettings>>().Value;
                Account account = new Account(config.CloudName, config.ApiKey, config.ApiSecret);
                return new Cloudinary(account);
            });
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
            services.AddHostedService<SubscriptionsCheckService>();
            services.AddHttpClient();
            services.AddScoped<IActorService, ActorService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<IDirectorService, DirectorService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IIMDBService, IMDBService>();
            services.AddScoped<ITVShowService, TVShowService>();
            services.AddScoped<ISeasonService, SeasonService>();
            services.AddScoped<IEpisodeService, EpisodeService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<LayoutService>();
            services.AddScoped<PayPalClient>();
            services.AddScoped<PhotoService>();
            services.AddScoped<VideoService>();
        }
    }
}
