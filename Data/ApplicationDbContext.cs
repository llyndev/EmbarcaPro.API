using EmbarcaPro.API.Data.Converters;
using EmbarcaPro.API.Models;

using Microsoft.EntityFrameworkCore;
using EmbarcaPro.API.Services.Interfaces;

namespace EmbarcaPro.API.Data
{
    public class ApplicationDbContext : DbContext
    {

        private readonly int _companyId;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUser currentUser) 
            : base(options)
        {
            _companyId = currentUser.CompanyId;
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Partner> Partners { get; set; }

        public DbSet<Driver> Drivers { get; set; }

        public DbSet<Truck> Trucks { get; set; }

        public DbSet<Trailer> Trailers { get; set; }

        public DbSet<Freight> Freights { get; set; }

        public DbSet<Cte> Ctes { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            // Toda data passa a ser gravada e lida como UTC.
            configurationBuilder.Properties<DateTime>()
                .HaveConversion<UtcDateTimeConverter>();

            configurationBuilder.Properties<DateTime?>()
                .HaveConversion<NullableUtcDateTimeConverter>();

            configurationBuilder.Properties<decimal>()
                .HavePrecision(18, 2);

            configurationBuilder.Properties<decimal?>()
                .HavePrecision(18, 2);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.Entity<User>().HasQueryFilter(u => u.CompanyId == _companyId);
            modelBuilder.Entity<Partner>().HasQueryFilter(p => p.CompanyId == _companyId);
            modelBuilder.Entity<Driver>().HasQueryFilter(d => d.CompanyId == _companyId);
            modelBuilder.Entity<Truck>().HasQueryFilter(t => t.CompanyId == _companyId);
            modelBuilder.Entity<Trailer>().HasQueryFilter(t => t.CompanyId == _companyId);
            modelBuilder.Entity<Freight>().HasQueryFilter(f => f.CompanyId == _companyId);
            modelBuilder.Entity<Cte>().HasQueryFilter(c => c.CompanyId == _companyId);

        }
    }
}
