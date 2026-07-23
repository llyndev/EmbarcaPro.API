using EmbarcaPro.API.Common.Helpers;
using EmbarcaPro.API.Enums;
using EmbarcaPro.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmbarcaPro.API.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.ToTable("users");

            builder.HasKey(user => user.Id);

            builder.Property(user => user.Id).HasColumnName("user_id");

            builder.Property(user => user.Name)
            .HasColumnName("name")
            .HasMaxLength(255)
            .IsRequired();

            builder.Property(user => user.Email)
            .HasColumnName("email")
            .HasMaxLength(255)
            .IsRequired();

            builder.Property(user => user.PasswordHash)
            .HasColumnName("password_hash")
            .IsRequired();

            builder.Property(user => user.Role)
            .HasColumnName("role")
            .HasConversion(new KeyDescriptionValueConverter<UserRole>(EmbarcaProEnumsList.GetUserRoles()))
            .HasMaxLength(5)
            .IsRequired();

            builder.Property(user => user.Active)
            .HasColumnName("active")
            .HasConversion(new KeyDescriptionValueConverter<UserStatus>(EmbarcaProEnumsList.GetUserStatus()))
            .HasMaxLength(5)
            .IsRequired();

            builder.Property(user => user.RegisterDate)
            .HasColumnName("register_date");

            builder.HasIndex(user => user.Email)
            .IsUnique();

        }

    }
}
