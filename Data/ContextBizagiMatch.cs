using Microsoft.EntityFrameworkCore;
using Model;

namespace Data
{
    public class ContextBizagiMatch : DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<PlayerMatchBid> PlayersMatchBids { get; set; }
        public DbSet<PlayersByGame> PlayersByGames { get; set; }

        public string ConnectionString { get; set; } = null;

        public ContextBizagiMatch() : base()
        {
        }

        public ContextBizagiMatch(DbContextOptions options) : base(options)
        {
        }

        public ContextBizagiMatch(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (ConnectionString != null)
                optionsBuilder.UseSqlServer(ConnectionString);
            else
                optionsBuilder.UseSqlServer("Data Source=MAOGAMER;Initial Catalog=BizagiMatchGameWeb; User ID=sa;Password=sa");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PlayersByGame>().HasKey(t => new { t.PlayerId, t.GameId });
            builder.Entity<PlayersByGame>().HasOne(t => t.Game).WithMany(t => t.Players).HasForeignKey(t => t.GameId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<PlayersByGame>().HasOne(t => t.Player).WithMany(t => t.Games).HasForeignKey(t => t.PlayerId);

            builder.Entity<Player>().HasMany(t => t.Characters).WithOne(t => t.Player);
            builder.Entity<Player>().HasMany(t => t.Games).WithOne(t => t.Player);

            builder.Entity<Game>().HasMany(t => t.Players).WithOne(t => t.Game);

            builder.Entity<Character>().HasMany(t => t.Weapons).WithOne(t => t.Character);
        }
    }
}