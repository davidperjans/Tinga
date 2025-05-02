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
    public class BidConfiguration : IEntityTypeConfiguration<Bid>
    {
        public void Configure(EntityTypeBuilder<Bid> builder)
        {
            // Primary Key
            builder.HasKey(b => b.Id);

            // Bid properties
            builder.Property(b => b.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(b => b.PlacedAt)
                .IsRequired();

            builder.Property(b => b.IsRetracted)
                .IsRequired();

            // Relations (Navigering)
            builder.HasOne(b => b.Listing)
                .WithMany(l => l.Bids)
                .HasForeignKey(b => b.ListingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.Bidder)
                .WithMany(u => u.Bids)
                .HasForeignKey(b => b.BidderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
