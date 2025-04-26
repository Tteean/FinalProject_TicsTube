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
        public DbSet<Language> Languages { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<MovieLanguage> MovieLanguages { get; set; }
        public DbSet<MovieComment> MovieComments { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<TVShow> TVShows { get; set; }
        public DbSet<TVShowActor> TVShowActors { get; set; }
        public DbSet<TVShowGenre> TVShowGenres { get; set; }
        public DbSet<TVShowLanguage> TVShowLanguages { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<MovieProduct> MovieProducts { get; set; }
        public DbSet<TvShowProduct> tvShowProducts { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }



        public TicsTubeDbContext(DbContextOptions options) : base(options)
        {
        }
        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries<Audit>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property(p => p.CreationDate).CurrentValue = DateTime.Now;
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Property(p => p.UpdatedDate).CurrentValue = DateTime.Now;
                }
                if (entry.Property(p => p.IsDeleted).CurrentValue == true)
                {
                    entry.Property(p => p.DeleteDate).CurrentValue = DateTime.Now;
                }
                if (entry.State == EntityState.Deleted)
                {
                    entry.Property(p => p.DeleteDate).CurrentValue = DateTime.Now;
                    entry.Property(p => p.IsDeleted).CurrentValue = true;
                    entry.State = EntityState.Modified;
                }
            }
            return base.SaveChanges();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Actor>().HasQueryFilter(x => !x.IsDeleted);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TicsTubeDbContext).Assembly);
        }
    }
}
