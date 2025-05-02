using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Bid
    {
        public Guid Id { get; set; }
        public Guid ListingId { get; set; }
        public Guid BidderId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PlacedAt { get; set; }
        public bool IsRetracted { get; set; }



        // Navigation Properties
        public Listing Listing { get; set; }
        public User Bidder { get; set; }
    }
}
