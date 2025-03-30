using FinalProject_Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_DataAccess.Data
{
    public class TicsTubeDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<MovieImage> MovieImages { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }


        public TicsTubeDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TicsTubeDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
