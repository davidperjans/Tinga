using Application.Common;
using Application.DTOs.Bid;
using Application.Features.Bids.Queries.GetListingBids;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.BidTests
{
    [TestFixture]
    public class GetListingBidsQueryHandlerTests
    {
        private GetListingBidsQueryHandler _handler;
        private Mock<IBidRepository> _mockBidRepository;
        private Mock<IRepository<Listing>> _mockListingRepository;
        private Mock<IMapper> _mockMapper;

        [SetUp]
        public void Setup()
        {
            _mockBidRepository = new Mock<IBidRepository>();
            _mockListingRepository = new Mock<IRepository<Listing>>();
            _mockMapper = new Mock<IMapper>();

            // Initialize the handler with the mocked dependencies
            _handler = new GetListingBidsQueryHandler(_mockBidRepository.Object, _mockMapper.Object, _mockListingRepository.Object);
        }

        [Test]
        public async Task Handle_ValidListingId_ReturnsListingBidsVm()
        {
            // Arrange
            var listingId = Guid.NewGuid();
            var listingTitle = "Test Listing";
            var query = new GetListingBidsQuery { ListingId = listingId };

            // Create an actual Listing instance instead of mocking it
            var listing = new Listing { Id = listingId, Title = listingTitle };

            _mockListingRepository.Setup(repo => repo.GetByIdAsync(listingId))
                .ReturnsAsync(OperationResult<Listing>.Success(listing));

            // Create actual Bid instances
            var bidder1Id = Guid.NewGuid();
            var bidder2Id = Guid.NewGuid();

            var bids = new List<Bid>
            {
                new Bid(listingId, Guid.NewGuid(), 100.50m),
                new Bid(listingId, Guid.NewGuid(), 150.75m)
            };

            _mockBidRepository.Setup(repo => repo.GetByListingIdAsync(listingId))
                .ReturnsAsync(OperationResult<IEnumerable<Bid>>.Success(bids));

            // Setup mapper to map bids to DTOs
            var bidDtos = new List<BidDto> { new BidDto(), new BidDto() };
            _mockMapper.Setup(m => m.Map<IList<BidDto>>(It.IsAny<IEnumerable<Bid>>()))
                .Returns(bidDtos);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Data);
            Assert.That(result.Data.ListingId, Is.EqualTo(listingId));
            Assert.That(result.Data.ListingTitle, Is.EqualTo(listingTitle));
            Assert.That(result.Data.Bids, Is.EqualTo(bidDtos));
        }

        [Test]
        public async Task Handle_ListingNotFound_ReturnsFailure()
        {
            // Arrange
            var listingId = Guid.NewGuid();
            var query = new GetListingBidsQuery { ListingId = listingId };

            // Setup listing repository to return failure
            _mockListingRepository.Setup(repo => repo.GetByIdAsync(listingId))
                .ReturnsAsync(OperationResult<Listing>.Failure("Listing not found"));

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.Message, Is.EqualTo("Listing not found"));
            Assert.IsNull(result.Data);
        }

        [Test]
        public async Task Handle_NoBidsFound_ReturnsFailure()
        {
            // Arrange
            var listingId = Guid.NewGuid();
            var listingTitle = "Test Listing";
            var query = new GetListingBidsQuery { ListingId = listingId };

            // Create an actual Listing instance instead of mocking it
            var listing = new Listing { Id = listingId, Title = listingTitle };

            _mockListingRepository.Setup(repo => repo.GetByIdAsync(listingId))
                .ReturnsAsync(OperationResult<Listing>.Success(listing));

            // Setup bid repository to return failure
            _mockBidRepository.Setup(repo => repo.GetByListingIdAsync(listingId))
                .ReturnsAsync(OperationResult<IEnumerable<Bid>>.Failure("No bids found for this listing"));

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.Message, Is.EqualTo("No bids found for this listing"));
            Assert.IsNull(result.Data);
        }
    }
}
