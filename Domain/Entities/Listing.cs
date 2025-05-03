using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class Listing
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal StartingPrice { get; private set; }
        public decimal CurrentPrice { get; private set; }
        public ListingStatus Status { get; private set; }
        public Guid CategoryId { get; private set; }
        public Guid SellerId { get; private set; }
        public DateTime EndTime { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public string ImageUrl { get; private set; }


        // Navigation properties
        public User Seller { get; private set; }
        public Category Category { get; private set; }
        public ICollection<Bid> Bids { get; private set; } = new List<Bid>();

        // Functions for controlling if bids is allowed
        public bool IsExpired() => DateTime.UtcNow > EndTime;
        public bool AcceptsBids() => !IsExpired() && Status == ListingStatus.Active;
    }
}
