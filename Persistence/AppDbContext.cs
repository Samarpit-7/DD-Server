
using DD_Server.Models;
using Microsoft.EntityFrameworkCore;

namespace DD_Server.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Audit> Audits { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Dictionary> Dictionary { get; set; }
        public DbSet<Request> Requests { get; set; }
       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Audit>().HasOne(e => e.User).WithMany().HasForeignKey(a => a.UId);
            modelBuilder.Entity<Dictionary>().HasOne(e => e.User).WithMany().HasForeignKey(a => a.UId);
            modelBuilder.Entity<Request>().HasOne(e => e.User).WithMany().HasForeignKey(a => a.UId);
            modelBuilder.Entity<Dictionary>()
                .HasIndex(d => d.DataPoint)
                .IsUnique();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public Dictionary GetByDataPoint(string dataPoint)
        {
            return Dictionary
                .FirstOrDefault(dictionary => dictionary.DataPoint == dataPoint);
        }
    }
}