using CRUDNet6Postgres.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDNet6Postgres.data
{
    public class ApiDBContext : DbContext
    {
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<DriverMedia> DriverMedias { get; set; }
        public ApiDBContext(DbContextOptions<ApiDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Driver>(entity => {
                entity.HasOne(t => t.Team)
                .WithMany(t => t.Drivers)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Driver_Team");

                entity.HasOne<DriverMedia>(dm => dm.DriverMedia)
                .WithOne(d => d.Driver)
                .HasForeignKey<DriverMedia>(dm => dm.DriverId);
            
            });

            
        }
    }
}
