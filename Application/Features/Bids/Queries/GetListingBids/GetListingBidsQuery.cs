using Application.Common;
using Application.DTOs.Bid;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Bids.Queries.GetListingBids
{
    public class GetListingBidsQuery : IRequest<OperationResult<ListingBidsVm>>
    {
        public Guid ListingId { get; set; }
    }
}
