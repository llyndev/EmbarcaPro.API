using EmbarcaPro.API.Data.Converters;
using EmbarcaPro.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EmbarcaPro.API.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Driver> Drivers { get; set; }

        public DbSet<Truck> Trucks { get; set; }

        public DbSet<Trailer> Trailers { get; set; }

        public DbSet<Facility> Facilities { get; set; }

        public DbSet<Freight> Freights { get; set; }

        public DbSet<Cte> Ctes { get; set; }

        public DbSet<CteFreightComponent> CteFreightComponents { get; set; }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            // Toda data passa a ser gravada e lida como UTC.
            configurationBuilder.Properties<DateTime>()
                .HaveConversion<UtcDateTimeConverter>();

            configurationBuilder.Properties<DateTime?>()
                .HaveConversion<NullableUtcDateTimeConverter>();

            configurationBuilder.Properties<Decimal>()
                .HavePrecision(18, 2);

            configurationBuilder.Properties<Decimal?>()
                .HavePrecision(18, 2);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        }
    }
}
