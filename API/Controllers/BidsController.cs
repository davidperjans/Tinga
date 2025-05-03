using Application.Features.Bids.Commands.CreateBid;
using Application.Features.Bids.Commands.RetractBid;
using Application.Features.Bids.Queries.GetListingBids;
using Application.Features.Bids.Queries.GetUserBids;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BidsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public BidsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("listing/{listingId}")]
        public async Task<IActionResult> GetListingBids(Guid listingId)
        {
            var query = new GetListingBidsQuery { ListingId = listingId };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserBids()
        {
            var query = new GetUserBidsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBid(CreateBidCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetListingBids), new { listingId = result.Data.ListingId }, result);
        }

        [HttpPut("{id}/retract")]
        public async Task<IActionResult> RetractBid(Guid id)
        {
            var command = new RetractBidCommand { BidId = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
