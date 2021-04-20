using System.Diagnostics.CodeAnalysis;
using ABB.NTier.Database.Configurations;
using ABB.NTier.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ABB.NTier.Database
{
    [ExcludeFromCodeCoverage]
    public class StorageContext : DbContext
    {
        public DbSet<Motor>         Motors        { get; set; }
        public DbSet<MeasuredValue> MeasuredValues { get; set; }

        public StorageContext() { }

        public StorageContext(DbContextOptions<StorageContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Host=localhost;Database=motors;Username=sa;Password=P@ssw0rd");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MotorConfiguration());
            modelBuilder.ApplyConfiguration(new MeasuredValueConfiguration());
        }
    }
}
