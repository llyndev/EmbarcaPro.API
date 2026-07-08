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

        public DbSet<Truck> Trucks { get; set; }

        public DbSet<Trailer> Trailers { get; set; }

        public DbSet<Facility> Facilities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        }
    }
}
