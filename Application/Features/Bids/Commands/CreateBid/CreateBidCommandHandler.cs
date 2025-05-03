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

namespace Application.Features.Bids.Commands.CreateBid
{
    public class CreateBidCommandHandler : IRequestHandler<CreateBidCommand, OperationResult<BidCreatedDto>>
    {
        private readonly IBidRepository _bidRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        public CreateBidCommandHandler(IBidRepository bidRepository, ICurrentUserService currentUserService, IMapper mapper)
        {
            _bidRepository = bidRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }
        public async Task<OperationResult<BidCreatedDto>> Handle(CreateBidCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (userId == Guid.Empty)
                return OperationResult<BidCreatedDto>.Failure("User not authenticated");

            var bid = new Bid(request.ListingId, userId, request.Amount);

            await _bidRepository.AddAsync(bid);

            var dto = _mapper.Map<BidCreatedDto>(bid);

            return OperationResult<BidCreatedDto>.Success(dto);
        }
    }
}
