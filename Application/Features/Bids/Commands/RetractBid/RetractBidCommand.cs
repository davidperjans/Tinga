using Application.Common;
using Application.DTOs.Bid;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Bids.Commands.RetractBid
{
    public class RetractBidCommand : IRequest<OperationResult<BidRetractedDto>>
    {
        public Guid BidId { get; set; }
    }
}
