using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Primary Key
            builder.HasKey(u => u.Id);

            // User properties
            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(200); // Om du lagrar hashade lösenord, ge tillräcklig längd

            builder.Property(u => u.FirstName)
                .HasMaxLength(100);

            builder.Property(u => u.LastName)
                .HasMaxLength(100);

            // Enum property
            builder.Property(u => u.Role)
                .HasConversion<string>();  // Konverterar UserRole till en string

            builder.Property(u => u.CreatedAt)
                .HasDefaultValueSql("GETDATE()"); // Default till dagens datum
            builder.Property(u => u.UpdatedAt)
                .HasDefaultValueSql("GETDATE()");

            // Relationer (Navigering)
            builder.HasMany(u => u.Listings)
                .WithOne(l => l.Seller)
                .HasForeignKey(l => l.SellerId);

            builder.HasMany(u => u.Bids)
                .WithOne(b => b.Bidder)
                .HasForeignKey(b => b.BidderId);
        }
    }
}
