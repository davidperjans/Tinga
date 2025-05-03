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

namespace Application.Features.Bids.Commands.RetractBid
{
    public class RetractBidCommandHandler : IRequestHandler<RetractBidCommand, OperationResult<BidRetractedDto>>
    {
        private readonly IBidRepository _bidRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly TimeSpan _retractWindow = TimeSpan.FromHours(1); // Can change
        public async Task<OperationResult<BidRetractedDto>> Handle(RetractBidCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (userId == Guid.Empty)
                return OperationResult<BidRetractedDto>.Failure("User not authenticated");

            // Get bid
            var bidResult = await _bidRepository.GetByIdAsync(request.BidId);

            if (!bidResult.IsSuccess || bidResult == null)
                return OperationResult<BidRetractedDto>.Failure("Bid not found");

            var bid = bidResult.Data;

            // Check if the bid belongs to the user
            if (bid.BidderId != userId)
                return OperationResult<BidRetractedDto>.Failure("Bid does not belong to the user");

            // Check if the bid can be retracted
            if (!bid.CanBeRetracted(_retractWindow))
                return OperationResult<BidRetractedDto>.Failure("Bid cannot be retracted after 1 hour");

            bid.Retract();

            await _bidRepository.UpdateAsync(bid);

            var dto = _mapper.Map<BidRetractedDto>(bid);

            return OperationResult<BidRetractedDto>.Success(dto);
        }
    }
}
