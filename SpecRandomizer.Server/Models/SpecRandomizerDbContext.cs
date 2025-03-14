using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SpecRandomizer.Server.Model;

namespace SpecRandomizer.Server.Models
{
    public class SpecRandomizerDbContext: DbContext
    {
        public SpecRandomizerDbContext(DbContextOptions<SpecRandomizerDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne<Role>()
                .WithMany()
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<UserRole>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<Configuration>()
                .HasOne(c => c.User)
                .WithMany(u => u.Configurations)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Player>()
                .HasOne(p => p.Configuration)
                .WithMany(c => c.Players)
                .HasForeignKey(p => p.ConfigurationId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    } 
}
