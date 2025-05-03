using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Bid
{
    public class BidDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRetracted { get; set; }
    }

    public class ListingBidsVm
    {
        public Guid ListingId { get; set; }
        public string ListingTitle { get; set; }
        public IList<BidDto> Bids { get; set; } = new List<BidDto>();
    }
}
