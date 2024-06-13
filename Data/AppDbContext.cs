using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
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
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Type = Models.Type.AcaoAventura },
                new Genre { Id = 2, Type = Models.Type.Anime },
                new Genre { Id = 3, Type = Models.Type.CriancasFamilia },
                new Genre { Id = 4, Type = Models.Type.Comedias },
                new Genre { Id = 5, Type = Models.Type.Crime },
                new Genre { Id = 6, Type = Models.Type.Documentarios },
                new Genre { Id = 7, Type = Models.Type.Dramas },
                new Genre { Id = 8, Type = Models.Type.Terror },
                new Genre { Id = 9, Type = Models.Type.Independente },
                new Genre { Id = 10, Type = Models.Type.FiccaoCientificaFantasia }
            );
            modelBuilder.Entity<TitleGenre>().HasKey(tg => new { tg.TitleId, tg.GenreId });
            modelBuilder.Entity<TitleGenre>().HasOne(tg => tg.Genre).WithMany(g => g.TitleGenres).HasForeignKey(tg => tg.GenreId);
            modelBuilder.Entity<TitleGenre>().HasOne(tg => tg.Title).WithMany(t => t.TitleGenres).HasForeignKey(tg => tg.TitleId);
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
            modelBuilder.Entity<User>().HasMany(u => u.MyList).WithMany(t => t.Users).UsingEntity(j => j.ToTable("UserTitles"));
        }
    }
}