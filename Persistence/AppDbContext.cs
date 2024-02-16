
using DD_Server.Model;
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
            modelBuilder.Entity<AppUser>().HasMany(e => e.Requests).WithOne(e => e.user).HasForeignKey(e => e.UId);
            modelBuilder.Entity<AppUser>().HasMany(e => e.Audits).WithOne(e => e.user).HasForeignKey(e => e.UId);
        }

        public Dictionary GetByDataPoint(string dataPoint)
        {
            return Dictionary
                .FirstOrDefault(dictionary => dictionary.DataPoint == dataPoint);
        }
    }
}