using EmbarcaPro.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmbarcaPro.API.Data.Mappings
{
    public class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {

        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.ToTable("drivers");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                .HasColumnName("driver_id");

            builder.Property(d => d.Name)
                .HasColumnName("name")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(d => d.Phone)
                .HasColumnName("phone")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(d => d.Email)
                .HasColumnName("email")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(d => d.Address)
                .HasColumnName("address")
                .IsRequired();

            builder.Property(d => d.Cpf)
                .HasColumnName("cpf")
                .HasMaxLength(11)
                .IsRequired();

            builder.Property(d => d.Cnh)
                .HasColumnName("cnh")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(d => d.IsActive)
                .HasColumnName("is_active")
                .HasDefaultValue(true);

            builder.Property(d => d.CreateAt)
                .HasColumnName("created_at");

            builder.HasIndex(d => d.Cpf)
                .IsUnique();

            builder.HasIndex(d => d.Cnh)
                .IsUnique();

            builder.HasIndex(d => d.Email)
                .IsUnique();

        }
    }
}
