using Application.Common;
using Application.DTOs.Bid;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Bids.Queries.GetListingBids
{
    public class GetListingBidsQueryHandler : IRequestHandler<GetListingBidsQuery, OperationResult<ListingBidsVm>>
    {
        private readonly IBidRepository _bidRepository;
        private readonly IRepository<Listing> _listingRepository;
        private readonly IMapper _mapper;
        public GetListingBidsQueryHandler(IBidRepository bidRepository, IMapper mapper, IRepository<Listing> listingRepository)
        {
            _bidRepository = bidRepository;
            _mapper = mapper;
            _listingRepository = listingRepository;
        }
        public async Task<OperationResult<ListingBidsVm>> Handle(GetListingBidsQuery request, CancellationToken cancellationToken)
        {
            // Get the listing to retrieve the title
            var listingResult = await _listingRepository.GetByIdAsync(request.ListingId);

            if (!listingResult.IsSuccess || listingResult.Data == null)
                return OperationResult<ListingBidsVm>.Failure("Listing not found");

            var listing = listingResult.Data;

            // Get the bids for the listing
            var bidsResult = await _bidRepository.GetByListingIdAsync(request.ListingId);

            if (!bidsResult.IsSuccess || bidsResult.Data == null)
                return OperationResult<ListingBidsVm>.Failure("No bids found for this listing");

            var bids = bidsResult.Data;


            // Map the bids to the DTO
            var vm = new ListingBidsVm
            {
                ListingId = request.ListingId,
                ListingTitle = listing.Title,
                Bids = _mapper.Map<IList<BidDto>>(bids)
            };

            return OperationResult<ListingBidsVm>.Success(vm);
        }
    }
}
