using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace UserApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<TitleGenre> TitleGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Episode>().HasOne(e => e.Title).WithMany(t => t.Episodes).HasForeignKey(e => e.TitleId);
            modelBuilder.Entity<TitleGenre>().HasKey(tg => new { tg.TitleId, tg.GenreId });
            modelBuilder.Entity<TitleGenre>().HasOne(tg => tg.Title).WithMany(t => t.TitleGenres).HasForeignKey(tg => tg.TitleId);
            modelBuilder.Entity<TitleGenre>().HasOne(tg => tg.Genre).WithMany(g => g.TitleGenres).HasForeignKey(tg => tg.GenreId);
            modelBuilder.Entity<User>().HasMany(u => u.MyList).WithMany(t => t.Users).UsingEntity(j => j.ToTable("UserTitles"));
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Action & Adventure" },
                new Genre { Id = 2, Name = "Anime" },
                new Genre { Id = 3, Name = "Children & Family" },
                new Genre { Id = 4, Name = "Comedies" },
                new Genre { Id = 5, Name = "Crime" },
                new Genre { Id = 6, Name = "Documentaries" },
                new Genre { Id = 7, Name = "Dramas" },
                new Genre { Id = 8, Name = "Horror" },
                new Genre { Id = 9, Name = "Independent" },
                new Genre { Id = 10, Name = "Sci-Fi & Fantasy" }
            );
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Email = "admin@example.com",
                    Username = "admin",
                    Password = "$2a$11$Ra1itzxCt0VdTW7UrQFDoehDSrLQwcIo/mzWoLZSnt83s/ZbgkGaC",
                    Role = Role.Admin,
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
}
