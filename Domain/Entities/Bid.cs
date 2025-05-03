using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Bid
    {
        public Guid Id { get; private set; }
        public Guid ListingId { get; private set; }
        public Guid BidderId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime PlacedAt { get; private set; }
        public bool IsRetracted { get; private set; }
        public DateTime? RetractedAt { get; private set; }



        // Navigation Properties
        public Listing Listing { get; private set; }
        public User Bidder { get; private set; }

        private Bid() { }

        public Bid(Guid listingId, Guid bidderId, decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Bid amount must be greater than zero", nameof(amount));

            Id = Guid.NewGuid();
            ListingId = listingId;
            BidderId = bidderId;
            Amount = amount;
            PlacedAt = DateTime.UtcNow;
            IsRetracted = false;
        }

        public void Retract()
        {
            if (IsRetracted)
                throw new InvalidOperationException("Bid has already been retracted");

            IsRetracted = true;
            RetractedAt = DateTime.UtcNow;
        }

        public bool CanBeRetracted(TimeSpan retractWindow)
        {
            if (IsRetracted)
                return false;

            return DateTime.UtcNow - PlacedAt <= retractWindow;
        }
    }
}
