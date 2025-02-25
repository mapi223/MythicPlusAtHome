using Microsoft.EntityFrameworkCore;
using SpecRandomizer.Server.Model;

namespace SpecRandomizer.Server.Models
{
    public class SpecRandomizerDbContext: DbContext
    {
        public SpecRandomizerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Configuration> Configurations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Configurations)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Configuration>()
                .HasMany(c => c.Players)
                .WithOne(p => p.Configuration)
                .HasForeignKey(p => p.ConfigurationId);
        }
    } 
}
