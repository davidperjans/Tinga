using Application.Common;
using Application.DTOs.Bid;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Bids.Queries.GetUserBids
{
    public class GetUserBidsQueryHandler : IRequestHandler<GetUserBidsQuery, OperationResult<UserBidsVm>>
    {
        private readonly IBidRepository _bidRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        public GetUserBidsQueryHandler(IBidRepository bidRepository, ICurrentUserService currentUserService, IMapper mapper)
        {
            _bidRepository = bidRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<OperationResult<UserBidsVm>> Handle(GetUserBidsQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (userId == Guid.Empty)
                return OperationResult<UserBidsVm>.Failure("User is not authenticated");

            // Get users bid
            var bidsResult = await _bidRepository.GetByUserIdAsync(userId);

            if (!bidsResult.IsSuccess || bidsResult == null)
                return OperationResult<UserBidsVm>.Failure("You have no bid");

            var bids = bidsResult.Data;

            // Create dtos with more information
            var bidDtos = new List<UserBidDto>();

            foreach (var bid in bids)
            {
                var highestBidResult = await _bidRepository.GetHighestBidForListingAsync(bid.ListingId);
                var dto = _mapper.Map<UserBidDto>(bid);

                if (highestBidResult.IsSuccess)
                {
                    dto.IsWinning = !bid.IsRetracted && bid.Amount == highestBidResult.Data;
                }
                else
                {
                    dto.IsWinning = false;
                }

                bidDtos.Add(dto);
            }

            return OperationResult<UserBidsVm>.Success(new UserBidsVm { Bids = bidDtos });
        }
    }
}
