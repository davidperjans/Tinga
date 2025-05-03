using Application.Common;
using Application.DTOs.Bid;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Bids.Commands.CreateBid
{
    public class CreateBidCommand : IRequest<OperationResult<BidCreatedDto>>
    {
        public Guid ListingId { get; set; }
        public decimal Amount { get; set; }
    }
}
