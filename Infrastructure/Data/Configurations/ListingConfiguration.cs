using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Configurations
{
    public class ListingConfiguration : IEntityTypeConfiguration<Listing>
    {
        public void Configure(EntityTypeBuilder<Listing> builder)
        {
            // Primary Key
            builder.HasKey(l => l.Id);

            // Listing properties
            builder.Property(l => l.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(l => l.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(l => l.StartingPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(l => l.CurrentPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(l => l.ImageUrl)
                .HasMaxLength(500);

            builder.Property(l => l.EndTime)
                .IsRequired();

            builder.Property(l => l.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(l => l.UpdatedAt)
                .HasDefaultValueSql("GETDATE()");

            // Enum property
            builder.Property(l => l.Status)
                .HasConversion<string>(); // Enum som string

            // Relations (Navigering)
            builder.HasOne(l => l.Seller)
                .WithMany(u => u.Listings)
                .HasForeignKey(l => l.SellerId);

            builder.HasOne(l => l.Category)
                .WithMany(c => c.Listings)  // Här anger vi den andra sidan av relationen
                .HasForeignKey(l => l.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);  // Förhindrar att kategorier tas bort om de har Listings

            builder.HasMany(l => l.Bids)
                .WithOne(b => b.Listing)
                .HasForeignKey(b => b.ListingId);
        }
    }
}
