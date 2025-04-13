using GameCatalogAPI.Entities;
using Microsoft.EntityFrameworkCore;
using static BCrypt.Net.BCrypt;

namespace GameCatalogAPI.DbContexts
{
    public class GameCatalogContext : DbContext
    {
        public GameCatalogContext(DbContextOptions<GameCatalogContext> options)
            : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Developer>().HasData(
                new Developer
                {
                    Id = 1,
                    Name = "Epic Games",
                    Founded = new DateOnly(1991, 1, 1),
                    Country = "USA",
                },
                new Developer
                {
                    Id = 2,
                    Name = "Nintendo",
                    Founded = new DateOnly(1889, 9, 23),
                    Country = "Japan"
                },
                new Developer
                {
                    Id = 3,
                    Name = "CD Projekt Red",
                    Founded = new DateOnly(2002, 5, 1),
                    Country = "Poland"
                },
                new Developer
                {
                    Id = 4,
                    Name = "Rockstar Games",
                    Founded = new DateOnly(1998, 12, 1),
                    Country = "USA"
                },
                new Developer
                {
                    Id = 5,
                    Name = "Valve",
                    Founded = new DateOnly(1996, 8, 24),
                    Country = "USA"
                });

            modelBuilder.Entity<Game>().HasData(
                new Game
                {
                    Id = 1,
                    Name = "Fortnite",
                    ReleaseDate = new DateOnly(2017, 7, 25),
                    Genre = "Battle Royale",
                    Platform = "PC",
                    Rating = 85,
                    DeveloperId = 1
                },
                new Game
                {
                    Id = 2,
                    Name = "The Legend of Zelda: Breath of the Wild",
                    ReleaseDate = new DateOnly(2017, 3, 3),
                    Genre = "Action-Adventure",
                    Platform = "Nintendo Switch",
                    Rating = 97,
                    DeveloperId = 2
                },
                new Game
                {
                    Id = 3,
                    Name = "The Witcher 3: Wild Hunt",
                    ReleaseDate = new DateOnly(2015, 5, 19),
                    Genre = "RPG",
                    Platform = "PC",
                    Rating = 93,
                    DeveloperId = 3
                },
                new Game
                {
                    Id = 4,
                    Name = "Cyberpunk 2077",
                    ReleaseDate = new DateOnly(2020, 12, 10),
                    Genre = "Action RPG",
                    Platform = "PC",
                    Rating = 75,
                    DeveloperId = 3
                },
                new Game
                {
                    Id = 5,
                    Name = "Grand Theft Auto V",
                    ReleaseDate = new DateOnly(2013, 9, 17),
                    Genre = "Open World",
                    Platform = "PC",
                    Rating = 96,
                    DeveloperId = 4
                },
                new Game
                {
                    Id = 6,
                    Name = "Red Dead Redemption 2",
                    ReleaseDate = new DateOnly(2018, 10, 26),
                    Genre = "Action-Adventure",
                    Platform = "PS4",
                    Rating = 97,
                    DeveloperId = 4
                },
                new Game
                {
                    Id = 7,
                    Name = "Half-Life 2",
                    ReleaseDate = new DateOnly(2004, 11, 16),
                    Genre = "FPS",
                    Platform = "PC",
                    Rating = 96,
                    DeveloperId = 5
                },
                new Game
                {
                    Id = 8,
                    Name = "Portal 2",
                    ReleaseDate = new DateOnly(2011, 4, 19),
                    Genre = "Puzzle",
                    Platform = "PC",
                    Rating = 95,
                    DeveloperId = 5
                });

            string adminPasswordHash = HashPassword("admin123");
            string covekPasswordHash = HashPassword("covek123");
            string admin2PasswordHash = HashPassword("keba123");

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    PasswordHash = adminPasswordHash,
                    Role = Role.Admin,
                    Age = 25
                },
                new User
                {
                    Id = 3,
                    Username = "keba",
                    PasswordHash = adminPasswordHash,
                    Role = Role.Admin,
                    Age = 16
                },
                new User
                {
                    Id = 2,
                    Username = "covek",
                    PasswordHash = covekPasswordHash,
                    Role = Role.User,
                    Age = 30
                });

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Game> Games { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
