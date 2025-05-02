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
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal StartingPrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public ListingStatus Status { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SellerId { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string ImageUrl { get; set; }


        // Navigation properties
        public User Seller { get; set; }
        public Category Category { get; set; }
        public ICollection<Bid> Bids { get; set; }
    }
}
