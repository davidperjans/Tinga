using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Bid
{
    public class UserBidDto
    {
        public Guid Id { get; set; }
        public Guid ListingId { get; set; }
        public string ListingTitle { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRetracted { get; set; }
        public bool IsWinning { get; set; }
    }

    public class UserBidsVm
    {
        public IList<UserBidDto> Bids { get; set; } = new List<UserBidDto>();
    }
}
