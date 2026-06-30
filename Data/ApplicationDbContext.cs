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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureUser(modelBuilder);

        }

        private static void ConfigureUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {

                entity.ToTable("users");

                entity.HasKey(user => user.Id);

                entity.Property(user => user.Id).HasColumnName("user_id");

                entity.Property(user => user.Name)
                .HasColumnName("name")
                .HasMaxLength(255)
                .IsRequired();

                entity.Property(user => user.Email)
                .HasColumnName("email")
                .HasMaxLength(255)
                .IsRequired();

                entity.Property(user => user.PasswordHash)
                .HasColumnName("password_hash")
                .IsRequired();

                entity.Property(user => user.Role)
                .HasColumnName("role")
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();

                entity.Property(user => user.Active)
                .HasColumnName("active");

                entity.Property(user => user.RegisterDate)
                .HasColumnName("register_date");

                entity.HasIndex(user => user.Email)
                .IsUnique();

            });

        }

    }
}
