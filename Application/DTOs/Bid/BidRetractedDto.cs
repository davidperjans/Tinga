using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Bid
{
    public class BidRetractedDto
    {
        public Guid Id { get; set; }
        public bool IsRetracted { get; set; }
        public DateTime? RetractedAt { get; set; }
    }
}
